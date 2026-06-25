using UnityEngine;
using UnityEngine.Video;

public class VictoryVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;


    void Start()
    {
        

        videoPlayer.Play();

        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        
    }
}
