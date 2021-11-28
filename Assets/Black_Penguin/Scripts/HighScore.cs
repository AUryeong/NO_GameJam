using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScore : Interaction
{
    [SerializeField] Image metch;
    [SerializeField] TextMeshProUGUI tmp;

    protected override void Action()
    {

    }
    private void OnMouseUp()
    {
        metch.gameObject.SetActive(false);
        tmp.gameObject.SetActive(true);
        tmp.text = Data.Instance.GetHighSocre().ToString();
    }
    private void OnMouseExit()
    {
        tmp.gameObject.SetActive(false);
        metch.gameObject.SetActive(true);
    }
}
