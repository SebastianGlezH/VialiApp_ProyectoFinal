using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InfoManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI textoNombre; // Texto para el nombre de la señal
    public TextMeshProUGUI textoDescripcion; // Texto para la descripción
    public Image imagenSeñal; // Imagen de la señal
    public Button btnAnterior; // Botón "Anterior"
    public Button btnSiguiente; // Botón "Siguiente"
    public TextMeshProUGUI textoProgreso; // Texto opcional para "1/10" (Opcional)

    [Header("Datos de Señales")]
    public SeñalSO[] señalesPreventivas; // Lista de señales preventivas
    public SeñalSO[] señalesRestrictivas; // Lista de señales restrictivas

    private SeñalSO[] señalesActuales; // Señales que se están mostrando actualmente
    private int indiceActual = 0; // Índice de la señal actual

    void Start()
    {
        // Cargar el tipo de contenido seleccionado (Preventivas/Restrictivas)
        string tipoContenido = PlayerPrefs.GetString("TipoContenido");

        // Asignar el conjunto de señales correspondiente
        señalesActuales = (tipoContenido == "Preventivas") ? señalesPreventivas : señalesRestrictivas;

        // Configurar listeners de los botones
        btnAnterior.onClick.AddListener(() => CambiarSeñal(-1));
        btnSiguiente.onClick.AddListener(() => CambiarSeñal(1));

        // Mostrar la primera señal
        MostrarSeñalActual();
    }

    void MostrarSeñalActual()
    {
        if (señalesActuales.Length == 0) return;

        // Obtener la señal actual
        SeñalSO señal = señalesActuales[indiceActual];

        // Actualizar UI
        textoNombre.text = señal.nombre;
        textoDescripcion.text = señal.descripcion;
        imagenSeñal.sprite = señal.imagen;

        // Actualizar progreso (opcional)
        if (textoProgreso != null)
            textoProgreso.text = $"{indiceActual + 1}/{señalesActuales.Length}";

        // Actualizar estado de los botones
        btnAnterior.interactable = (indiceActual > 0);
        btnSiguiente.interactable = (indiceActual < señalesActuales.Length - 1);
    }

    void CambiarSeñal(int cambio)
    {
        // Cambiar el índice y asegurarse de que esté dentro de los límites
        indiceActual = Mathf.Clamp(indiceActual + cambio, 0, señalesActuales.Length - 1);

        // Mostrar la nueva señal
        MostrarSeñalActual();
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Escena_Menu");
    }
}