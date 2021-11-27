using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : BaseObject
{
    public Sprite defaultsprite;
    public Sprite opensprite;
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = defaultsprite;
    }
    public override void OnHammer()
    {
        base.OnHammer();
        gameObject.GetComponent<SpriteRenderer>().sprite = opensprite;
    }
    public override void OnMetch()
    {
        base.OnMetch();
        GameObject.Destroy(gameObject);
    }
}
