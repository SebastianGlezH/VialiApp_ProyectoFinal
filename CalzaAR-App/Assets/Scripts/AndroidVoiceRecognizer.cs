using UnityEngine;
using UnityEngine.UI;

public class AndroidVoiceRecognizer : MonoBehaviour
{
    private AndroidJavaObject activity;
    private AndroidJavaObject speechPlugin;

    [SerializeField] private Text recognizedText;

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            speechPlugin = new AndroidJavaObject("com.tuempresa.speechplugin.SpeechPlugin", activity);
        }
#endif
    }

    public void StartListening()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        speechPlugin.Call("startListening");
#endif
    }

    // Este método lo llama Android con el resultado
    public void OnSpeechResult(string result)
    {
        Debug.Log("Reconocido: " + result);
        if (recognizedText != null)
            recognizedText.text = result;
    }
}
