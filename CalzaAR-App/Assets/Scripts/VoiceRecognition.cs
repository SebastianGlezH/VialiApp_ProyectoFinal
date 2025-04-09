using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
using UnityEngine.Windows.Speech;
#endif

public class VoiceRecognition : MonoBehaviour
{
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
#endif

    [SerializeField] private Image micIcon;
    [SerializeField] private Color listeningColor = Color.white;
    private Color defaultColor;

    [SerializeField] private Button[] buttonsToDisable;

    private AndroidJavaObject speechPlugin;

    void Start()
    {
        if (micIcon != null)
            defaultColor = micIcon.color;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        keywords.Add("señales", () =>
        {
            SceneManager.LoadScene("SMenu");
        });

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
#elif UNITY_ANDROID && !UNITY_EDITOR
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            speechPlugin = new AndroidJavaObject("android.speech.SpeechRecognizer", activity);
        }
#endif
    }

    public void ToggleMicrophone()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        if (keywordRecognizer.IsRunning)
        {
            keywordRecognizer.Stop();
            SetMicState(false);
        }
        else
        {
            keywordRecognizer.Start();
            SetMicState(true);
        }
#elif UNITY_ANDROID && !UNITY_EDITOR
        StartAndroidVoiceRecognition();
        SetMicState(true);
#endif
    }

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
#endif

    private void SetMicState(bool isListening)
    {
        if (micIcon != null)
            micIcon.color = isListening ? listeningColor : defaultColor;

        foreach (Button button in buttonsToDisable)
        {
            if (button != null)
                button.interactable = !isListening;
        }
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void StartAndroidVoiceRecognition()
    {
        using (AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", "android.speech.action.RECOGNIZE_SPEECH"))
        {
            intent.Call<AndroidJavaObject>("putExtra", "android.speech.extra.LANGUAGE_MODEL", "free_form");
            intent.Call<AndroidJavaObject>("putExtra", "android.speech.extra.LANGUAGE", "es-MX");

            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call("startActivityForResult", intent, 10); // 10 = request code
            }
        }
    }

    // Este método es llamado desde Android con el resultado (debes usar un plugin si quieres esto)
    public void OnSpeechResult(string recognizedText)
    {
        Debug.Log("Texto reconocido: " + recognizedText);
        if (recognizedText.ToLower().Contains("señales"))
        {
            SceneManager.LoadScene("SMenu");
        }

        SetMicState(false);
    }
#endif

    void OnDestroy()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
#endif
    }
}
