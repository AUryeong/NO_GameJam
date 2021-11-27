using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : BaseObject
{
    public Sprite defaultsprite;
    public Sprite opensprite;
    public Sprite firedsprite;
    public bool opend = false;
    public bool fired = false;
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = defaultsprite;
    }
    public override void OnHammer()
    {
        base.OnHammer();
        if (!opend && !fired)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = opensprite;
            opend = true;
            Data.Instance.score += 50;
        }
    }
    public override void OnMetch()
    {
        base.OnMetch();
        if(!opend && !fired)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = firedsprite;
            fired = true;
            Data.Instance.score -= 50;
        }
    }
}
