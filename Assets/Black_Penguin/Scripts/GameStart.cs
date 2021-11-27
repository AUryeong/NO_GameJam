using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : Interaction
{
    [SerializeField] Image flash;
    protected override void Action()
    {
        StartCoroutine(StartScene());
    }

    IEnumerator StartScene()
    {
        for (int i = 0; i < 10; i++)
        {
            flash.color = new Color(1, 1, 1, i * 0.1f);
            yield return new WaitForSeconds(0.02f);
        }
        for (int i = 10; i > 0; i--)
        {
            flash.color = new Color(1, 1, 1, i * 0.1f);
            yield return new WaitForSeconds(0.02f);
        }

        SceneManager.LoadScene("C-GameIntro");
    }

    void Update()
    {

    }
}
