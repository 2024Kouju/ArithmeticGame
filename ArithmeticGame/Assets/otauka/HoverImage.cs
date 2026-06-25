using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverFrame : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public Image frameImage;

    public Color hoverColor = Color.yellow;
    public Color clickColor = Color.red;

    private bool clicked = false;

    void Start()
    {
        frameImage.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (clicked) return;

        frameImage.gameObject.SetActive(true);
        frameImage.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (clicked) return;

        frameImage.gameObject.SetActive(false);
    }

    public void Click()
    {
        clicked = true;

        frameImage.gameObject.SetActive(true);
        frameImage.color = clickColor;
    }

    public void ResetFrame()
    {
        clicked = false;
        frameImage.gameObject.SetActive(false);
    }
}