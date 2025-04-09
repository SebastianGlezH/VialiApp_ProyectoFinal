using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class VoiceRecognition : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    [SerializeField] private Image micIcon; // Asigna el icono del micr�fono desde el Inspector
    [SerializeField] private Color listeningColor = Color.white; // Color cuando est� escuchando
    private Color defaultColor; // Guarda el color original del icono

    [SerializeField] private Button[] buttonsToDisable; // Botones que se desactivar�n al escuchar

    void Start()
    {
        // Guarda el color original del icono
        if (micIcon != null)
            defaultColor = micIcon.color;

        // Palabra clave y acci�n asociada
        keywords.Add("se�ales", () =>
        {
            // Cargar la escena SMenu
            SceneManager.LoadScene("SMenu");
        });

        // Configurar el reconocedor
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
    }

    // M�todo para activar/desactivar el micr�fono (ll�malo desde el bot�n)
    public void ToggleMicrophone()
    {
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
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    // Cambia el estado del micr�fono (escuchando o no)
    private void SetMicState(bool isListening)
    {
        // Cambia el color del icono
        if (micIcon != null)
            micIcon.color = isListening ? listeningColor : defaultColor;

        // Desactiva otros botones mientras escucha
        foreach (Button button in buttonsToDisable)
        {
            if (button != null)
                button.interactable = !isListening;
        }
    }

    void OnDestroy()
    {
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }
}