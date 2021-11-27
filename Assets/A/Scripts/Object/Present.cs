using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : BaseObject
{
    void Start()
    {
        OnMetch();
    }
    public override void OnHammer()
    {
        base.OnHammer();
    }
    public override void OnMetch()
    {
        base.OnMetch();
        GameObject obj = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Fire"), gameObject.transform.parent);
        obj.transform.localPosition = gameObject.transform.localPosition;
        obj.transform.localScale = gameObject.transform.localScale / 14;
        obj.transform.localRotation = gameObject.transform.localRotation;
        GameObject.Destroy(gameObject);
    }
}
