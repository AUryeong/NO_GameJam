using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GridState
{
    BLANK,
    STOVE,
    COUPLE,
    GIFT,
    PLAYER
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
                    if(cur_cnt == stove_rand)
                    {
                        InGameGrid[first_x + x, first_y + y] = GridState.STOVE;
                        GridObjList[first_x + x, first_y + y].GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                        goto A;
                    }
                    else if(x == 0 && y == 0 && stove_rand == 0)
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

        int couple_rand_X = Random.Range(0, 12);
        int couple_rand_Y = Random.Range(0, 8);
        while (InGameGrid[couple_rand_X, couple_rand_Y] != GridState.BLANK)
        {

        }
    }

    void Start()
    {

    }

    void Update()
    {
        PlayerMove();
        PlayerPosMove();
    }

    void PlayerMove()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (player_y > 0)
            {
                player_y--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (player_y < Grid_Y - 1)
            {
                player_y++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (player_x > 0)
            {
                player_x--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (player_x < Grid_X - 1)
            {
                player_x++;
            }
        }

        InGameGrid[player_x, player_y] = GridState.PLAYER;
    }

    void PlayerPosMove()
    {
        Vector3 playerPos = new Vector3(player_x - 7, player_y - 4, 0);
        player.transform.position = playerPos;
    }
}
