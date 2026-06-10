using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverSE : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip hoverSE;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(hoverSE);
    }
}