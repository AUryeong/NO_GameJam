using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public int stage; //range to 1~8

    public int coupleCnt;
    public int giftCnt;

    public Sprite gift_default;
    public Sprite gift_opened;

    protected override void Awake()
    {
        GridInitialSetting();
    }

    void GridInitialSetting()
    {
        for (int x = 0; x < Grid_X; x++)
        {
            for (int y = 0; y < Grid_Y; y++)
            {
                InGameGrid[x, y] = GridState.BLANK;
                Vector3 grid_pos = new Vector3(x - 7, y - 4, 0);
                GridObjList[x, y] = Instantiate(GridPrefab, grid_pos, Quaternion.identity, transform);
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
                        GridObjList[first_x + x, first_y + y].GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                        GridObjList[first_x + x, first_y + y].AddComponent<Stove>();
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
            GridObjList[couple_rand_X, couple_rand_Y].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
            GridObjList[couple_rand_X, couple_rand_Y].AddComponent<Couple>();
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
            GridObjList[gift_rand_X, gift_rand_Y].GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
            GridObjList[gift_rand_X, gift_rand_Y].AddComponent<Present>();
            GridObjList[gift_rand_X, gift_rand_Y].GetComponent<Present>().defaultsprite = gift_default;
            GridObjList[gift_rand_X, gift_rand_Y].GetComponent<Present>().opensprite = gift_opened;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        PlayerMove();
        PlayerPosMove();
        PlayerAttack();
    }

    void PlayerAttack()
    {
        GameObject[] near_player = new GameObject[4];
        near_player = ReturnGridArray();

        if (Input.GetKeyDown(KeyCode.Z)) //metch attack
        {
            for (int i = 0; i < near_player.Length; i++)
            {
                if (near_player[i] != null)
                    near_player[i].GetComponent<BaseObject>().OnMetch();
            }
        }
        else if (Input.GetKeyDown(KeyCode.X)) //hammer attack
        {
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

        if (player_x - 1 > 0 && InGameGrid[player_x - 1, player_y] != GridState.BLANK)
            near_player_grid[0] = GridObjList[player_x - 1, player_y];
        else near_player_grid[0] = null;

        if (player_x + 1 < Grid_X - 1 && InGameGrid[player_x + 1, player_y] != GridState.BLANK)
            near_player_grid[1] = GridObjList[player_x + 1, player_y];
        else near_player_grid[1] = null;

        if (player_y - 1 > 0 && InGameGrid[player_x, player_y - 1] != GridState.BLANK)
            near_player_grid[2] = GridObjList[player_x, player_y - 1];
        else near_player_grid[2] = null;

        if (player_y + 1 < Grid_Y - 1 && InGameGrid[player_x, player_y + 1] != GridState.BLANK)
            near_player_grid[3] = GridObjList[player_x, player_y + 1];
        else near_player_grid[3] = null;

        return near_player_grid;
    }

    void PlayerMove()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (InGameGrid[player_x, player_y - 1] != GridState.BLANK && player_y - 1 > 0)
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
            if (InGameGrid[player_x, player_y + 1] != GridState.BLANK && player_y + 1 < Grid_Y - 1)
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
            if (InGameGrid[player_x - 1, player_y] != GridState.BLANK && player_x - 1 > 0)
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
            if (InGameGrid[player_x + 1, player_y] != GridState.BLANK && player_x + 1 < Grid_X - 1)
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
        Vector3 playerPos = new Vector3(player_x - 7, player_y - 4, 0);
        player.transform.position = playerPos;
    }
}
