using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;
public class VideoPlayerManager : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [Header("UI")]
    public GameObject openVideoButton;
    public GameObject videoPanel;
    public GameObject darkOverlay;

    [Header("常時表示UI")]
    public Slider seekSlider;
    public Button closeButton;

    [Header("中央アイコン")]
    public GameObject centerIconObject;
    public Image centerIconImage;
    public Sprite playSprite;   // ▶
    public Sprite pauseSprite;  // ⏸

    [Header("Video")]
    public VideoPlayer videoPlayer;
    [Header("枠演出")]
    public Image frameImage;

    public Color hoverColor = Color.yellow;
    public Color clickColor = Color.red;
    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip buttonSound;

    [Header("動画を開くまでの待機時間")]
    public float openDelay = 1f;

    [Header("無効化するボタン")]
    public Button[] buttons;

    private bool isDragging = false;
    private bool isProcessing = false;
    private Coroutine iconCoroutine;

    public HoverFrame hoverFrame;
    void Start()
    {
        videoPanel.SetActive(false);
        darkOverlay.SetActive(false);
        centerIconObject.SetActive(false);
        if (frameImage != null)
        {
            frameImage.gameObject.SetActive(false);
        }
        videoPlayer.loopPointReached += OnVideoFinished;

        seekSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void Update()
    {
        if (videoPlayer.isPrepared &&
            videoPlayer.length > 0 &&
            !isDragging)
        {
            seekSlider.value =
                (float)(videoPlayer.time / videoPlayer.length);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (frameImage == null || isProcessing) return;

        frameImage.gameObject.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (frameImage == null || isProcessing) return;

        frameImage.gameObject.SetActive(true);
        frameImage.color = hoverColor;
    }
    // 「動画を見る」
    public void OpenVideo()
    {
        hoverFrame.Click();

        if (isProcessing) return;

        isProcessing = true;

        // 全ボタンを無効化
        foreach (Button btn in buttons)
        {
            btn.interactable = false;
        }

        // 赤枠表示
        if (frameImage != null)
        {
            frameImage.gameObject.SetActive(true);
            frameImage.color = clickColor;
        }

        if (audioSource != null && buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }

        StartCoroutine(OpenVideoAfterDelay());
    }

    IEnumerator OpenVideoAfterDelay()
    {
        yield return new WaitForSeconds(openDelay);

        if (frameImage != null)
        {
            hoverFrame.ResetFrame();
        }

        openVideoButton.SetActive(false);

        videoPanel.SetActive(true);

        darkOverlay.SetActive(false);
        centerIconObject.SetActive(false);

        videoPlayer.time = 0;
        videoPlayer.Play();

        isProcessing = false;
    }

    public void CloseVideo()
    {
        videoPlayer.Stop();

        videoPanel.SetActive(false);
        darkOverlay.SetActive(false);
        centerIconObject.SetActive(false);

        openVideoButton.SetActive(true);

        // 全ボタンを有効化
        foreach (Button btn in buttons)
        {
            btn.interactable = true;
        }

        isProcessing = false;
    }

    // 動画タップ
    public void TogglePlayPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();

            ShowCenterIcon(playSprite);
        }
        else
        {
            videoPlayer.Play();

            ShowCenterIcon(pauseSprite);
        }
    }

    // シーク開始
    public void DragStart()
    {
        isDragging = true;

        darkOverlay.SetActive(true);

        videoPlayer.Pause();
    }

    // シーク終了
    public void DragEnd()
    {
        videoPlayer.time =
            seekSlider.value * videoPlayer.length;

        isDragging = false;

        darkOverlay.SetActive(false);

        videoPlayer.Play();
    }

    // シーク中
    void OnSliderValueChanged(float value)
    {
        if (isDragging && videoPlayer.length > 0)
        {
            videoPlayer.time =
                value * videoPlayer.length;
        }
    }

    // 中央アイコン表示
    void ShowCenterIcon(Sprite icon)
    {
        centerIconImage.sprite = icon;

        centerIconObject.SetActive(true);

        if (iconCoroutine != null)
        {
            StopCoroutine(iconCoroutine);
        }

        iconCoroutine = StartCoroutine(HideCenterIcon());
    }

    IEnumerator HideCenterIcon()
    {
        yield return new WaitForSeconds(1f);

        centerIconObject.SetActive(false);

        iconCoroutine = null;
    }

    // 動画終了
    void OnVideoFinished(VideoPlayer vp)
    {
        videoPlayer.Pause();

        seekSlider.value = 1f;

        ShowCenterIcon(playSprite);
    }
}