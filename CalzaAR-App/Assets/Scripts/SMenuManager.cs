using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SMenuManager : MonoBehaviour
{
    public static SMenuManager Instance { get; private set; }

    [Header("Botones")]
    public Button btnPreventivas;
    public Button btnRestrictivas;
    public Button btnInformativas;
    public Button btnTestGeneral;

    [Header("Colores")]
    public Color colorBloqueado = Color.gray;
    public Color colorActivo = Color.white;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("Progreso guardado al iniciar: " + PlayerPrefs.GetInt("LeccionCompletada", 0));

        // Configurar listeners para cada botón
        btnPreventivas.onClick.AddListener(() => CargarInformacion("Preventivas"));
        btnRestrictivas.onClick.AddListener(() => CargarInformacion("Restrictivas"));
        btnInformativas.onClick.AddListener(() => CargarInformacion("Informativas"));
        btnTestGeneral.onClick.AddListener(() => CargarTestGeneral());

        ActualizarBotones();
    }

    private void ActualizarBotones()
    {
        int leccionCompletada = PlayerPrefs.GetInt("LeccionCompletada", 0);

        // Preventivas siempre activa
        btnPreventivas.interactable = true;
        btnPreventivas.image.color = colorActivo;

        // Restrictivas se activa con el nivel 1
        btnRestrictivas.interactable = (leccionCompletada >= 1);
        btnRestrictivas.image.color = btnRestrictivas.interactable ? colorActivo : colorBloqueado;

        // Informativas se activa con el nivel 2
        btnInformativas.interactable = (leccionCompletada >= 2);
        btnInformativas.image.color = btnInformativas.interactable ? colorActivo : colorBloqueado;

        // Test General se activa con el nivel 3
        if (btnTestGeneral != null)
        {
            btnTestGeneral.gameObject.SetActive(leccionCompletada >= 3);
        }
    }

    public void CargarInformacion(string tipoContenido)
    {
        PlayerPrefs.SetString("TipoContenido", tipoContenido);
        SceneManager.LoadScene("Escena_Informacion");
    }

    public void CargarTestGeneral()
    {
        PlayerPrefs.SetString("TipoContenido", "TestGeneral");
        SceneManager.LoadScene("Escena_Tests");
    }

    public void CompletarLeccion(int leccionIndex)
    {
        int nivelActual = PlayerPrefs.GetInt("LeccionCompletada", 0);

        if (leccionIndex > nivelActual)
        {
            PlayerPrefs.SetInt("LeccionCompletada", leccionIndex);
            PlayerPrefs.Save();
            Debug.Log("Progreso actualizado a nivel: " + leccionIndex);
        }
        ActualizarBotones();
    }
}