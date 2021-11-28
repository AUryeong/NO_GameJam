using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameIntroUIManager : MonoBehaviour
{

    public Sprite[] toonSprite;
    public Image toonImage;

    public Button sceneChangeButton;

    void Start()
    {
        sceneChangeButton.gameObject.SetActive(false);
        StartCoroutine(Manga());
    }

    void Update()
    {
        
    }

    public void sceneChange()
    {
        SceneManager.LoadScene("D-InGameScene");
    }

    IEnumerator Manga()
    {
        for (int a = 0; a < 3; a++)
        {
            yield return new WaitForSeconds(2);
            toonImage.sprite = toonSprite[a];
        }

        int i = 3;
        int max = toonSprite.Length;
        sceneChangeButton.gameObject.SetActive(true);
        while (true)
        {
            if (i < max - 1) i++;
            else i = 3;
            toonImage.sprite = toonSprite[i];
            yield return new WaitForSeconds(0.05f);
        }
    }
}
