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
        for (int a = 0; a < 2; a++)
        {
            toonImage.sprite = toonSprite[a];
            yield return new WaitForSeconds(2f);
        }

        int i = 2;
        int max = toonSprite.Length;
        sceneChangeButton.gameObject.SetActive(true);
        while (true)
        {
            if (i < max - 1) i++;
            else i = 2;
            toonImage.sprite = toonSprite[i];
            yield return new WaitForSeconds(0.05f);
        }
    }
}
