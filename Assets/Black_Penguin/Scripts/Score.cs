using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] Text score;
    [SerializeField] Text Level;
    void Start()
    {
        
    }

    void Update()
    {
        score.text = "Score : " + Data.Instance.score;
        Level.text = "Level : " + Data.Instance.stage;
    }
}
