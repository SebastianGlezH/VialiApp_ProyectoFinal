using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InfoManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI textoNombre; // Texto para el nombre de la señal
    public TextMeshProUGUI textoDescripcion; // Texto para la descripción
    public Image imagenSenal; // Imagen de la señal
    public Button btnAnterior; // Botón "Anterior"
    public Button btnSiguiente; // Botón "Siguiente"
    public TextMeshProUGUI textoProgreso; // Texto opcional para "1/10" (Opcional)

    [Header("Datos de Señales")]
    public SenalSO[] senalesPreventivas; // Lista de señales preventivas
    public SenalSO[] senalesRestrictivas; // Lista de señales restrictivas

    private SenalSO[] senalesActuales; // Señales que se están mostrando actualmente
    private int indiceActual = 0; // Índice de la señal actual

    void Start()
    {
        // Cargar el tipo de contenido seleccionado (Preventivas/Restrictivas)
        string tipoContenido = PlayerPrefs.GetString("TipoContenido");

        // Asignar el conjunto de señales correspondiente
        senalesActuales = (tipoContenido == "Preventivas") ? senalesPreventivas : senalesRestrictivas;

        // Configurar listeners de los botones
        btnAnterior.onClick.AddListener(() => CambiarSenal(-1));
        btnSiguiente.onClick.AddListener(() => CambiarSenal(1));

        // Mostrar la primera señal
        MostrarSenalActual();
    }

    void MostrarSenalActual()
    {
        if (senalesActuales.Length == 0) return;

        // Obtener la señal actual
        SenalSO senal = senalesActuales[indiceActual];

        // Actualizar UI
        textoNombre.text = senal.nombre;
        textoDescripcion.text = senal.descripcion;
        imagenSenal.sprite = senal.imagen;

        // Actualizar progreso (opcional)
        if (textoProgreso != null)
            textoProgreso.text = $"{indiceActual + 1}/{senalesActuales.Length}";

        // Actualizar estado de los botones
        btnAnterior.interactable = (indiceActual > 0);
        btnSiguiente.interactable = (indiceActual < senalesActuales.Length - 1);
    }

    void CambiarSenal(int cambio)
    {
        // Cambiar el índice y asegurarse de que está dentro de los límites
        indiceActual = Mathf.Clamp(indiceActual + cambio, 0, senalesActuales.Length - 1);

        // Mostrar la nueva señal
        MostrarSenalActual();
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Escena_Menu");
    }
}