using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quit : Interaction
{
    [SerializeField] Image hammer;
    protected override void Action()
    {
        Application.Quit();
        SoundManager.Instance.ChangeClip("버튼클릭", false);

    }
    private void OnMouseOver()
    {
        hammer.transform.position = new Vector3(transform.position.x - 2, transform.position.y, 0);
    }
}
