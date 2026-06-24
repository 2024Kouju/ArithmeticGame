using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerManager : MonoBehaviour
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

    private bool isDragging = false;
    private Coroutine iconCoroutine;

    void Start()
    {
        videoPanel.SetActive(false);
        darkOverlay.SetActive(false);
        centerIconObject.SetActive(false);

        videoPlayer.loopPointReached += OnVideoFinished;

        seekSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void Update()
    {
        // 動画の再生位置をシークバーへ反映
        if (videoPlayer.isPrepared &&
            videoPlayer.length > 0 &&
            !isDragging)
        {
            seekSlider.value =
                (float)(videoPlayer.time / videoPlayer.length);
        }
    }

    // 「動画を見る」
    public void OpenVideo()
    {
        openVideoButton.SetActive(false);

        videoPanel.SetActive(true);

        darkOverlay.SetActive(false);
        centerIconObject.SetActive(false);

        videoPlayer.time = 0;
        videoPlayer.Play();
    }

    // ×ボタン
    public void CloseVideo()
    {
        videoPlayer.Stop();

        videoPanel.SetActive(false);
        darkOverlay.SetActive(false);
        centerIconObject.SetActive(false);

        openVideoButton.SetActive(true);
    }

    // 動画画面タップ
    public void TogglePlayPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();

            // 停止したので▶表示
            ShowCenterIcon(playSprite);
        }
        else
        {
            videoPlayer.Play();

            // 再生したので⏸表示
            ShowCenterIcon(pauseSprite);
        }
    }

    // シーク開始
    public void DragStart()
    {
        isDragging = true;

        // 少し暗くする
        darkOverlay.SetActive(true);

        // 一旦停止
        videoPlayer.Pause();
    }

    // シーク終了
    public void DragEnd()
    {
        videoPlayer.time =
            seekSlider.value * videoPlayer.length;

        isDragging = false;

        darkOverlay.SetActive(false);

        // その位置から再生
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
        // 動画の最後のフレームで停止
        videoPlayer.Pause();

        // シークバーを最後まで進める
        seekSlider.value = 1f;

        // 停止アイコン（▶）を表示
        ShowCenterIcon(playSprite);
    }
}