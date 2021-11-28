using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Developers : Interaction
{
    [SerializeField] Image metch;
    [SerializeField] Image hammer;
    [SerializeField] TextMeshProUGUI tmp;

    protected override void Action()
    {
        SoundManager.Instance.ChangeClip("버튼클릭", false);

    }
    private void OnMouseUp()
    {
        hammer.transform.position = new Vector3(transform.position.x - 2, transform.position.y, 0);
        metch.gameObject.SetActive(false);
        tmp.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        tmp.gameObject.SetActive(false);
        metch.gameObject.SetActive(true);
    }
}
