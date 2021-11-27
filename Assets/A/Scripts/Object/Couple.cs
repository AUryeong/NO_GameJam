using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Couple : BaseObject
{
    public Sprite sprite;
    public Sprite angry;
    public Sprite mindcontrol;
    public bool mindcontrolling = false;
    public bool angrying = false;
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public override void OnHammer()
    {
        base.OnHammer();
        if (!mindcontrolling && ! angrying)
        {
            InGameManager.Instance.matchGauge -= 5;
            StartCoroutine(Hammer());
        }
    }
    IEnumerator Hammer()
    {
        angrying = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = angry;
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        angrying = false;
        yield return null;
    }
    public override void OnMetch()
    {
        base.OnMetch();
        if (!mindcontrolling && !angrying)
        {
            StartCoroutine(Metch());
        }
    }
    IEnumerator Metch()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = mindcontrol;
        mindcontrolling = true;
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        mindcontrolling = false;
        yield return null;
    }
}
