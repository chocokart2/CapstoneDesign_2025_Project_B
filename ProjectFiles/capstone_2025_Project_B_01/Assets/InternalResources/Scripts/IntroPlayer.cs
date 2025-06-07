using UnityEngine;
using UnityEngine.Video;

public class IntroPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.Play();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // 영상 끝났을 때 다음 씬으로 이동
        UnityEngine.SceneManagement.SceneManager.LoadScene("GeunohTest1");
    }
}