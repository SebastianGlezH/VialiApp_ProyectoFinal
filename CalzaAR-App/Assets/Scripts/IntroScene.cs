using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroLoader : MonoBehaviour
{
    public string nextSceneName = "Inicio"; // Cambia esto por el nombre real de tu siguiente escena

    void Start()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
