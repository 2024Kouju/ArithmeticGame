using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour,
     IPointerEnterHandler,
    IPointerExitHandler
{
    public string NextSceneText;

    private AudioSource audioSource;

    public AudioClip ButtonSound;
    // 操作禁止にしたいボタン
    public Button[] buttons;

    // 連打防止フラグ    // 枠のImage
    public Image frameImage;

    // 色設定
    public Color hoverColor = Color.yellow;
    public Color clickColor = Color.red;

    private bool isClicked = false;

    private bool isProcessing = false;

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

        

        // 既に処理中なら何もしない
        if (isProcessing) return;

        isProcessing = true;
        isClicked = true;
        // 全ボタンを無効化
        foreach (Button btn in buttons)
        {
            btn.interactable = false;
        }
        // 赤枠表示
        frameImage.gameObject.SetActive(true);
        frameImage.color = clickColor;
        audioSource.PlayOneShot(ButtonSound);

        Invoke("Change", 1f);
    }

    public void Change()
    {
        SceneManager.LoadScene(NextSceneText);
    }
}
