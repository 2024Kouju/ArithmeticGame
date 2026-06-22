using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerManager : MonoBehaviour
{
    public GameObject openVideoButton;
    public GameObject videoPanel;
    public GameObject controls;

    public VideoPlayer videoPlayer;

    public Image playPauseImage;
    public Sprite playSprite;
    public Sprite pauseSprite;

    private Coroutine hideCoroutine;

    void Start()
    {
        videoPanel.SetActive(false);
        controls.SetActive(false);

        videoPlayer.loopPointReached += OnVideoFinished;

        UpdateIcon();
    }

    // 「動画を見る」
    public void OpenVideo()
    {
        openVideoButton.SetActive(false);

        videoPanel.SetActive(true);

        videoPlayer.time = 0;
        videoPlayer.Play();

        UpdateIcon();
    }

    // ×ボタン
    public void CloseVideo()
    {
        videoPlayer.Stop();

        videoPanel.SetActive(false);

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
        controls.SetActive(true);

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        hideCoroutine = StartCoroutine(HideControls());
    }

    System.Collections.IEnumerator HideControls()
    {
        yield return new WaitForSeconds(3f);

        controls.SetActive(false);
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