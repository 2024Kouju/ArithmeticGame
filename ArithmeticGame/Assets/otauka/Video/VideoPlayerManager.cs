using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerManager : MonoBehaviour
{
    public GameObject openVideoButton;
    public GameObject videoPanel;
    public GameObject controls;
    public GameObject darkOverlay;
    public VideoPlayer videoPlayer;
    public Image playPauseImage;
    public Sprite playSprite;
    public Sprite pauseSprite;

    private Coroutine hideCoroutine;

    void Start()
    {
        videoPanel.SetActive(false);
        controls.SetActive(false);
        darkOverlay.SetActive(false);

        videoPlayer.loopPointReached += OnVideoFinished;

        UpdateIcon();
    }

    // 「動画を見る」
    // 「動画を見る」
    public void OpenVideo()
    {
        openVideoButton.SetActive(false);

        videoPanel.SetActive(true);

        videoPlayer.time = 0;
        videoPlayer.Play();

        // 最初は停止ボタン（⏸）を表示
        playPauseImage.sprite = pauseSprite;
    }

    // ×ボタン
    public void CloseVideo()
    {
        videoPlayer.Stop();

        videoPanel.SetActive(false);

        controls.SetActive(false);
        darkOverlay.SetActive(false);

        openVideoButton.SetActive(true);

        UpdateIcon();
    }

    // ▶/⏸ボタン
    public void TogglePlayPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }

        UpdateIcon();
    }

    // 動画画面タップ
    public void ShowControls()
    {
        // 表示中なら非表示
        if (controls.activeSelf)
        {
            controls.SetActive(false);
            darkOverlay.SetActive(false);

            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
                hideCoroutine = null;
            }

            return;
        }

        // 非表示なら表示
        controls.SetActive(true);
        darkOverlay.SetActive(true);

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        hideCoroutine = StartCoroutine(HideControls());
    }

    System.Collections.IEnumerator HideControls()
    {
        yield return new WaitForSeconds(6f);

        controls.SetActive(false);
        darkOverlay.SetActive(false);

        hideCoroutine = null;
    }

    void UpdateIcon()
    {
        if (videoPlayer.isPlaying)
        {
            playPauseImage.sprite = pauseSprite;
        }
        else
        {
            playPauseImage.sprite = playSprite;
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        CloseVideo();
    }
}