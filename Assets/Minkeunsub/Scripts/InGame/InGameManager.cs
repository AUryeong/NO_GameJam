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


    public PlayerController player;

    int player_x, player_y;

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
            }
        }

        player_x = 0;
        player_y = 0;
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
        Vector3 playerPos = new Vector3(player_x - 7, player_y -  4, 0);
        player.transform.position = playerPos;
    }
}
