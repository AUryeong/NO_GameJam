using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class Interaction : MonoBehaviour
{
    Button button;
    protected virtual void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => { Action(); });
    }
    protected abstract void Action();
}
