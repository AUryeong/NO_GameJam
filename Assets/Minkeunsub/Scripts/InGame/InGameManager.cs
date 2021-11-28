using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum GridState
{
    BLANK,
    STOVE,
    COUPLE,
    COUPLESTUN,
    GIFT,
}

public class InGameManager : Singleton<InGameManager>
{
    public static int Grid_X = 12;
    public static int Grid_Y = 8;
    public GridState[,] InGameGrid = new GridState[Grid_X, Grid_Y];
    public GameObject[,] GridObjList = new GameObject[Grid_X, Grid_Y];


    public PlayerController player;

    int player_x, player_y;
    int first_x, first_y;

    public GameObject GridPrefab;
    private float MatchDuration;

    public int stage; //range to 1~8
    public float score;

    public int coupleCnt;
    public int giftCnt;
    public bool starting = false;

    public static float maxMatch = 150;
    public float matchGauge;

    [Header("UI Objects")]
    public Image hpBar;
    public Text stageTxt;
    public Text scoreTxt;
    public GameObject gameend;

    [Header("Sprites")]
    public Sprite gift_default;
    public Sprite gift_opened;
    public Sprite gift_fired;
    public Sprite stove_active;
    public Sprite stove_off;
    public Sprite stove_bomb;
    public Sprite couple_default;
    public Sprite couple_heart;
    public Sprite couple_angry;
    public Sprite couple_mindcontrol;
    public Sprite[] grass;


    public void GameEnd()
    {
        Time.timeScale = 0;
        SoundManager.Instance.ChangeClip("게임오버", false);

        starting = false;
        if(Data.Instance.GetHighSocre() <= Data.Instance.score)
        {
            Data.Instance.SetHighscore(Data.Instance.score);
        }
        Data.Instance.score = 0;
        gameend.SetActive(true);
    }

    protected override void Awake()
    {
        gameend.SetActive(false);
        GridInitialSetting();
    }
    
    void GridInitialSetting()
    {
        stage = Data.Instance.stage;
        Stove.stoves = new List<Stove>();
        MatchDuration = 0;
        matchGauge = maxMatch;
        for (int x = 0; x < Grid_X; x++)
        {
            for (int y = 0; y < Grid_Y; y++)
            {
                InGameGrid[x, y] = GridState.BLANK;
                Vector3 grid_pos = new Vector3(x - 7 + 1.46f, y - 4, 0);
                GridObjList[x, y] = Instantiate(GridPrefab, grid_pos, Quaternion.identity, transform);
                float random = Random.Range(0, 100);
                if (random <= 70)
                {
                    GridObjList[x, y].GetComponent<SpriteRenderer>().sprite = grass[2];
                }
                else if (random <= 85)
                {
                    GridObjList[x, y].GetComponent<SpriteRenderer>().sprite = grass[0];
                }
                else
                {
                    GridObjList[x, y].GetComponent<SpriteRenderer>().sprite = grass[1];
                }
            }
        }

        for (int i = 0; i < stage; i++)
        {
        B:;
            first_x = i > 2 ? (i - 3) * 4 : i * 4;
            first_y = (int)(i / 3) * 4;

            int stove_rand = Random.Range(0, 16);
            int cur_cnt = 0;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (cur_cnt == stove_rand)
                    {
                        InGameGrid[first_x + x, first_y + y] = GridState.STOVE;
                        GridObjList[first_x + x, first_y + y].AddComponent<Stove>();
                        GridObjList[first_x + x, first_y + y].GetComponent<Stove>().activestove = stove_active;
                        GridObjList[first_x + x, first_y + y].GetComponent<Stove>().offstove = stove_off;
                        GridObjList[first_x + x, first_y + y].GetComponent<Stove>().bombstove = stove_bomb;
                        goto A;
                    }
                    else if (x == 0 && y == 0 && stove_rand == 0)
                    {
                        goto B;
                    }
                    else
                    {
                        cur_cnt++;
                    }
                }
            }
        A:;
        }

        player_x = 0;
        player_y = 0;
        starting = true;

        for (int i = 0; i < coupleCnt; i++)
        {
            int couple_rand_X = Random.Range(0, 12);
            int couple_rand_Y = Random.Range(0, 8);
            while (InGameGrid[couple_rand_X, couple_rand_Y] != GridState.BLANK)
            {
                couple_rand_X = Random.Range(0, 12);
                couple_rand_Y = Random.Range(0, 8);
            }

            InGameGrid[couple_rand_X, couple_rand_Y] = GridState.COUPLE;
            GridObjList[couple_rand_X, couple_rand_Y].AddComponent<Couple>();
            GridObjList[couple_rand_X, couple_rand_Y].GetComponent<Couple>().sprite = (Random.Range(1,3) == 1) ? couple_heart: couple_default;
            GridObjList[couple_rand_X, couple_rand_Y].GetComponent<Couple>().mindcontrol = couple_mindcontrol;
            GridObjList[couple_rand_X, couple_rand_Y].GetComponent<Couple>().angry = couple_angry;
        }

        for (int i = 0; i < giftCnt; i++)
        {
            int gift_rand_X = Random.Range(0, 12);
            int gift_rand_Y = Random.Range(0, 8);
            while (InGameGrid[gift_rand_X, gift_rand_Y] != GridState.BLANK)
            {
                gift_rand_X = Random.Range(0, 12);
                gift_rand_Y = Random.Range(0, 8);
            }

            InGameGrid[gift_rand_X, gift_rand_Y] = GridState.GIFT;
            GridObjList[gift_rand_X, gift_rand_Y].AddComponent<Present>();
            GridObjList[gift_rand_X, gift_rand_Y].GetComponent<Present>().defaultsprite = gift_default;
            GridObjList[gift_rand_X, gift_rand_Y].GetComponent<Present>().opensprite = gift_opened;
            GridObjList[gift_rand_X, gift_rand_Y].GetComponent<Present>().firedsprite = gift_fired;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if (starting)
        {
            PlayerMove();
            PlayerPosMove();
            PlayerAttack();
            MatchesControll();
            UIControll();
            ClearCheck();
        }
    }

    void UIControll()
    {
        score = Data.Instance.score;
        stageTxt.text = "Stage " + stage.ToString();
        scoreTxt.text = "Score: " + score.ToString();
    }

    void ClearCheck()
    {
        if (Stove.stoves.Count >= Singleton<Data>.Instance.stage)
        {
            Data.Instance.score += stage * 100;
            Data.Instance.score += (int)matchGauge;
            Singleton<Data>.Instance.stage++;
            SceneManager.LoadScene("D-InGameScene");
        }
    }
    void MatchesControll()
    {
        MatchDuration += Time.deltaTime;
        if(MatchDuration >= 1)
        {
            MatchDuration = 0;
            matchGauge--;
        }
        hpBar.fillAmount = matchGauge / maxMatch;
    }

    void PlayerAttack()
    {
        GameObject[] near_player = new GameObject[4];
        near_player = ReturnGridArray();

        if (Input.GetKeyDown(KeyCode.Z)) //metch attack
        {
            Debug.Log("성냥");
            SoundManager.Instance.ChangeClip("성냥", false);

            player.MatchAttack();
            for (int i = 0; i < near_player.Length; i++)
            {
                if (near_player[i] != null)
                    near_player[i].GetComponent<BaseObject>().OnMetch();
            }
        }
        if (Input.GetKeyDown(KeyCode.X)) //hammer attack
        {
            Debug.Log("망치");
            SoundManager.Instance.ChangeClip("망치", false);

            player.HammerAttack();
            matchGauge -= 5;
            for (int i = 0; i < near_player.Length; i++)
            {
                if (near_player[i] != null)
                    near_player[i].GetComponent<BaseObject>().OnHammer();
            }
        }
    }

    GameObject[] ReturnGridArray()
    {
        GameObject[] near_player_grid = new GameObject[4];

        if (player_x - 1 >= 0 && InGameGrid[player_x - 1, player_y] != GridState.BLANK)
            near_player_grid[0] = GridObjList[player_x - 1, player_y];
        else near_player_grid[0] = null;

        if (player_x + 1 < Grid_X && InGameGrid[player_x + 1, player_y] != GridState.BLANK)
            near_player_grid[1] = GridObjList[player_x + 1, player_y];
        else near_player_grid[1] = null;

        if (player_y - 1 >= 0 && InGameGrid[player_x, player_y - 1] != GridState.BLANK)
            near_player_grid[2] = GridObjList[player_x, player_y - 1];
        else near_player_grid[2] = null;

        if (player_y + 1 < Grid_Y && InGameGrid[player_x, player_y + 1] != GridState.BLANK)
            near_player_grid[3] = GridObjList[player_x, player_y + 1];
        else near_player_grid[3] = null;

        return near_player_grid;
    }

    void PlayerMove()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SoundManager.Instance.ChangeClip("이동", false);

            if (player_y > 0 && InGameGrid[player_x, player_y - 1] == GridState.GIFT)
            {
                if (GridObjList[player_x, player_y - 1].GetComponent<Present>().fired)
                {
                    player_y--;
                }
                else
                {
                    return;
                }
            }
            else if (player_y > 0 && InGameGrid[player_x, player_y - 1] == GridState.COUPLE)
            {
                if (GridObjList[player_x, player_y - 1].GetComponent<Couple>().mindcontrolling)
                {
                    player_y--;
                }
                else
                {
                    return;
                }
            }
            else if (player_y - 1 < 0 || InGameGrid[player_x, player_y - 1] != GridState.BLANK)
            {
                return;
            }
            else if (player_y > 0)
            {
                player_y--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundManager.Instance.ChangeClip("이동", false);

            if (player_y < Grid_Y - 1 && InGameGrid[player_x, player_y + 1] == GridState.GIFT)
            {
                if (GridObjList[player_x, player_y + 1].GetComponent<Present>().fired)
                {
                    player_y++;
                }
                else
                {
                    return;
                }
            }
            else if (player_y < Grid_Y - 1 && InGameGrid[player_x, player_y + 1] == GridState.COUPLE)
            {
                if (GridObjList[player_x, player_y + 1].GetComponent<Couple>().mindcontrolling)
                {
                    player_y++;
                }
                else
                {
                    return;
                }
            }
            else if (player_y + 1 >= Grid_Y || InGameGrid[player_x, player_y + 1] != GridState.BLANK)
            {
                return;
            }
            else if (player_y < Grid_Y - 1)
            {
                player_y++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SoundManager.Instance.ChangeClip("이동", false);

            if (player_x > 0 && InGameGrid[player_x - 1, player_y] == GridState.GIFT)
            {
                if (GridObjList[player_x - 1, player_y].GetComponent<Present>().fired)
                {
                    player_x--;
                }
                else
                {
                    return;
                }
            }
            else if (player_x > 0 && InGameGrid[player_x - 1, player_y] == GridState.COUPLE)
            {
                if (GridObjList[player_x - 1, player_y].GetComponent<Couple>().mindcontrolling)
                {
                    player_x--;
                }
                else
                {
                    return;
                }
            }
            else if (player_x - 1 < 0 || InGameGrid[player_x - 1, player_y] != GridState.BLANK)
            {
                return;
            }
            else if (player_x > 0)
            {
                player_x--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SoundManager.Instance.ChangeClip("이동", false);

            if (player_x < Grid_X - 1 && InGameGrid[player_x + 1, player_y] == GridState.GIFT)
            {
                if (GridObjList[player_x + 1, player_y].GetComponent<Present>().fired)
                {
                    player_x++;
                }
                else
                {
                    return;
                }
            }
            else if (player_x < Grid_X - 1 && InGameGrid[player_x + 1, player_y] == GridState.COUPLE)
            {
                if (GridObjList[player_x + 1, player_y].GetComponent<Couple>().mindcontrolling)
                {
                    player_x++;
                }
                else
                {
                    return;
                }
            }
            else if (player_x + 1 >= Grid_X || InGameGrid[player_x + 1, player_y] != GridState.BLANK)
            {
                return;
            }
            else if (player_x < Grid_X - 1)
            {
                player_x++;
            }
        }
    }

    void PlayerPosMove()
    {
        Vector3 playerPos = new Vector3(player_x - 7 + 1.46f, player_y - 4, 0);
        player.transform.position = playerPos;
    }
}
