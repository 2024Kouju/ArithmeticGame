using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Goingtoschool : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public string NextSceneText;

    private AudioSource audioSource;

    public AudioClip ButtonSound;

    // 操作禁止にしたいボタン
    public Button[] buttons;

    // 連打防止フラグ
    private bool isProcessing = false;
    // 枠のImage
    public Image frameImage;

    // 色設定
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

       

        // 既に処理中なら何もしない
        if (isProcessing) return;

        isProcessing = true;

        isClicked = true;

        // 赤枠表示
        frameImage.gameObject.SetActive(true);
        frameImage.color = clickColor;

        // 全ボタンを無効化
        foreach (Button btn in buttons)
        {
            btn.interactable = false;
        }

        audioSource.PlayOneShot(ButtonSound);

        Invoke(nameof(Change), 5.5f);
    }

    public void Change()
    {
        SceneManager.LoadScene(NextSceneText);
    }
}