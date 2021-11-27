using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : Interaction
{
    protected override void Action()
    {
        Application.Quit();
    }
}
