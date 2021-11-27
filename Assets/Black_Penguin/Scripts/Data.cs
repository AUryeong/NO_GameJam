using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : Singleton<Data>
{
    public int score;
    public int stage = 1;

    public void SetHighscore(int value)
    {
        PlayerPrefs.SetInt("Highscore", value);
    }
    public int GetHighSocre()
    {
        return PlayerPrefs.GetInt("Highscore");
    }
    void Stageset(int value)
    {
        PlayerPrefs.SetInt("Stage", value);
    } 
}
