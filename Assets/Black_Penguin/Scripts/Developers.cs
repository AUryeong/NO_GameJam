using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Developers : Interaction
{
    [SerializeField] Image developers;
    bool isActive;
    protected override void Action()
    {

        developers.gameObject.SetActive(true);
        isActive = true;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isActive)
            {
                developers.gameObject.SetActive(false);
            }
        }
    }
}
