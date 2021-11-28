using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : Interaction
{
    protected override void Action()
    {
        Application.Quit();
        SoundManager.Instance.ChangeClip("버튼클릭", false);

    }
}
