using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SMenuManager : MonoBehaviour
{
    [Header("Botones")]
    public Button btnPreventivas;
    public Button btnRestrictivas;
    public Button btnInformativas;  // Bot�n de Informativas (sin Tur�sticas)

    [Header("Colores")]
    public Color colorBloqueado = Color.gray;
    public Color colorActivo = Color.white;

    void Start()
    {
        // Configurar listeners para cada bot�n
        btnPreventivas.onClick.AddListener(() => CargarInformacion("Preventivas"));
        btnRestrictivas.onClick.AddListener(() => CargarInformacion("Restrictivas"));
        btnInformativas.onClick.AddListener(() => CargarInformacion("Informativas"));

        // Inicializar estado de los botones
        ActualizarBotones();
    }

    // M�todo para cargar la escena de informaci�n
    void CargarInformacion(string tipoContenido)
    {
        PlayerPrefs.SetString("TipoContenido", tipoContenido);
        SceneManager.LoadScene("Escena_Informacion");
    }

    private void ActualizarBotones()
    {
        int leccionCompletada = PlayerPrefs.GetInt("LeccionCompletada", 0);

        // Preventivas siempre activa
        btnPreventivas.interactable = true;
        btnPreventivas.image.color = colorActivo;

        // Restrictivas
        btnRestrictivas.interactable = (leccionCompletada >= 1);
        btnRestrictivas.image.color = btnRestrictivas.interactable ? colorActivo : colorBloqueado;

        // Informativas
        btnInformativas.interactable = (leccionCompletada >= 2);
        btnInformativas.image.color = btnInformativas.interactable ? colorActivo : colorBloqueado;
    }

    public void CompletarLeccion(int leccionIndex)
    {
        PlayerPrefs.SetInt("LeccionCompletada", leccionIndex);
        PlayerPrefs.Save();
        ActualizarBotones();
    }

    // M�todos para completar lecciones
    public void OnLeccionPreventivasCompletada()
    {
        CompletarLeccion(1); // Desbloquea Restrictivas
    }

    public void OnLeccionRestrictivasCompletada()
    {
        CompletarLeccion(2); // Desbloquea Informativas (�ltimo nivel)
    }

    // Eliminado OnLeccionInformativasCompletada() ya que es el �ltimo nivel
}