using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroUIManager : MonoBehaviour
{
    public Sprite[] sprites;
    public Image mangaImg;

    void Start()
    {
        SoundManager.Instance.ChangeClip("메인브금", true);
        StartCoroutine(Manga());
    }

    void Update()
    {

    }

    public void MoveScene()
    {
        SceneManager.LoadScene("B-MainScene");
    }

    IEnumerator Manga()
    {
        int i = 0;
        while (true)
        {
            if (i < sprites.Length - 1) i++;
            else i = 0;

            mangaImg.sprite = sprites[i];

            yield return new WaitForSeconds(0.05f);
        }
    }
}
