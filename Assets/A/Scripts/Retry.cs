using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Retry : Interaction
{
    protected override void Action()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("B-MainScene");
    }
}
