using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("‰Ÿ‚µ‚½");
        UIManager.Instance.ShowPanel();
    }
}
