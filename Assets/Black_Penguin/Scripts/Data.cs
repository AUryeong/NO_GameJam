using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : Singleton<Data>
{
    public int score;
    public int stage = 1;

    void Highscoreset(int value)
    {
        PlayerPrefs.SetInt("Highscore",value);
    }
    void Stageset(int value)
    {
        PlayerPrefs.SetInt("Stage", value);
    } 
}
