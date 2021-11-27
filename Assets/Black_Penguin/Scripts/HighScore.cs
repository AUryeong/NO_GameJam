using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : Interaction
{
    protected override void Action()
    {
        Debug.Log(Data.Instance.GetHighSocre());
    }
}
