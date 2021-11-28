using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{

    public GameObject howToPlay;

    void Start()
    {
        howToPlay.SetActive(false);
    }

    void Update()
    {
        
    }

    public void SetOff()
    {
        howToPlay.SetActive(false);
    }

    public void SetOn()
    {
        howToPlay.SetActive(true);
    }
}
