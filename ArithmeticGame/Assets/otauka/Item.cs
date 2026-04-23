using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    public GameObject panel;

    public void SetPanel(GameObject p)
    {
        panel = p;
    }

    void OnMouseDown()
    {
        panel.SetActive(true);
    }

}
