using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Stove : BaseObject
{
    public static int MaxGage = 20;
    public static List<Stove> stoves = new List<Stove>();
    public int Gage = 0;
    public bool active = true;
    public Sprite activestove;
    public Sprite offstove;
    public Sprite bombstove;
    private Image Gagebar;
    private float activeduration = 0;
    private float duration = 0;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = activestove;
    }
    void Update()
    {
        if(Gagebar == null)
        {
            Gagebar = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Gagebar"), GameObject.Find("Stove GageBarParent").gameObject.transform).transform.GetChild(0).GetComponent<Image>();
        }
        if (active)
        {
            duration += Time.deltaTime;
            if (duration >= 1)
            {
                Gage += Random.Range(2, 5);
                duration = 0;
            }
            if (Gage >= 20)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = bombstove;
                Singleton<InGameManager>.Instance.GameEnd();
                //여기다가 게임오버 넣어주세요
            }
        }
        else
        {
            activeduration += Time.deltaTime;
            if (activeduration >= 5)
            {
                active = true;
                activeduration = 0;
                duration = 0;
                if (stoves.Contains(this))
                {
                    stoves.Remove(this);
                }
                gameObject.GetComponent<SpriteRenderer>().sprite = activestove;
            }
        }
        if(Gagebar != null)
        {
            Gagebar.GetComponent<RectTransform>().parent.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 0.6f, 0));
            Gagebar.fillAmount = (float) Gage / MaxGage;
        }
    }

    private void Awake()
    {
    }
    public override void OnHammer()
    {
        base.OnHammer();
        Gage = Mathf.Max(0, Gage-5);
        if (Gage <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = offstove;
            if (!stoves.Contains(this))
            {
                stoves.Add(this);
            }
            active = false;
        }
    }
    public override void OnMetch()
    {
        base.OnMetch();
        Gage = Mathf.Min(MaxGage, Gage + 5);
    }
}
