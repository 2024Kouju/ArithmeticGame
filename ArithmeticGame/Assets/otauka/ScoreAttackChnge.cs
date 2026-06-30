using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreAttackChange : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public string NextSceneText;

    // 操作禁止にしたいボタン
    public Button[] buttons;

    // 連打防止
    private bool isProcessing = false;

    private AudioSource audioSource;

    public AudioClip ButtonSound;

    // 枠のImage
    public Image frameImage;

    // 色設定
    public Color hoverColor = Color.yellow;
    public Color clickColor = Color.red;

    private bool isClicked = false;
    void Start()
    {
        // AudioSourceコンポーネントを取得
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
        // 連打防止
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

        Quiz1.Boss = false;
        Quiz5.Boss5 = false;
        Quiz10.Boss10 = false;
        Quiz25.Boss25 = false;
        Quiz50.Boss50 = false;
        Quiz100.Boss100 = false;

        Quiz1.Score = true;
        Quiz5.Score5 = true;
        Quiz10.Score10 = true;
        Quiz25.Score25 = true;
        Quiz50.Score50 = true;
        Quiz100.Score100 = true;

        audioSource.PlayOneShot(ButtonSound);

        Invoke(nameof(Change), 1f);
    }

    public void Change()
    {
        SceneManager.LoadScene(NextSceneText);
    }
}
