using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Sprite sprite;
    [SerializeField] Sprite sprite2;
    [SerializeField] Sprite sprite3;
    [SerializeField] Sprite sprite4;
    [SerializeField] Sprite sprite5;
    public void MatchAttack()
    {
        StopCoroutine(Match());
        StopCoroutine(Hammer());
        StartCoroutine(Match());
    }
    public void HammerAttack()
    {
        StopCoroutine(Match());
        StopCoroutine(Hammer());
        StartCoroutine(Hammer());
    }
    IEnumerator Match()
    {
        SoundManager.Instance.ChangeClip("성냥", false);

        gameObject.GetComponent<SpriteRenderer>().sprite = sprite4;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite5;
        yield return null;
    }
    IEnumerator Hammer()
    {
        SoundManager.Instance.ChangeClip("망치", false);

        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite3;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite5;
        yield return null;
    }
    private void Start()
    {
        SoundManager.Instance.ChangeClip("인게임브금", true);

    }
}
