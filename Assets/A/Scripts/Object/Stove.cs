using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : BaseObject
{
    public static int MaxGage = 20;
    public int Gage = 0;
    public bool active = true;
    private float activeduration;
    private float duration;

    void Update()
    {
        if (active)
        {
            if (Gage <= 0)
            {
                active = false;
            }
            else
            {
                duration += Time.deltaTime;
                if (duration >= 1)
                {
                    Gage += Random.Range(2, 5);
                }
                if(Gage >= 20)
                {
                    //여기다가 게임오버 넣어주세요
                }
            }
        }
        else
        {
            activeduration += Time.deltaTime;
            if (activeduration >= 5)
            {
                active = true;
                duration = 0;
            }
        }
    }
    public override void OnHammer()
    {
        Gage = Mathf.Max(0, Gage-5);
    }
    public override void OnMetch()
    {
        Gage = Mathf.Min(MaxGage, Gage + 5);
    }
}
