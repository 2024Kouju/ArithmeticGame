using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoosAttackChange : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public string NextSceneText;

    private AudioSource audioSource;
    public AudioClip ButtonSound;

    // ògÇÃImage
    public Image frameImage;

    // êFê›íË
    public Color hoverColor = Color.yellow;
    public Color clickColor = Color.red;

    private bool isClicked = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (frameImage != null)
        {
            frameImage.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isClicked) return;

        frameImage.gameObject.SetActive(true);
        frameImage.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isClicked) return;

        frameImage.gameObject.SetActive(false);
    }

    public void NextScene()
    {
        isClicked = true;

        // ê‘ògï\é¶
        frameImage.gameObject.SetActive(true);
        frameImage.color = clickColor;

        Quiz1.Score = false;
        Quiz5.Score5 = false;
        Quiz10.Score10 = false;
        Quiz25.Score25 = false;
        Quiz50.Score50 = false;
        Quiz100.Score100 = false;

        Quiz1.Boss = true;
        Quiz5.Boss5 = true;
        Quiz10.Boss10 = true;
        Quiz25.Boss25 = true;
        Quiz50.Boss50 = true;
        Quiz100.Boss100 = true;

        audioSource.PlayOneShot(ButtonSound);

        Invoke(nameof(Change), 1f);
    }

    public void Change()
    {
        SceneManager.LoadScene(NextSceneText);
    }
}