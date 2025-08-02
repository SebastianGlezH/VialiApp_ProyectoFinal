using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InfoManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoDescripcion;
    public Image imagenSenal;
    public Button btnAnterior;
    public Button btnSiguiente;
    public TextMeshProUGUI textoProgreso;
    public Button btnFinalizarLeccion; // Nuevo botón

    [Header("Datos de Señales")]
    public SenalSO[] senalesPreventivas;
    public SenalSO[] senalesRestrictivas;

    private SenalSO[] senalesActuales;
    private int indiceActual = 0;

    void Start()
    {
        string tipoContenido = PlayerPrefs.GetString("TipoContenido");
        senalesActuales = (tipoContenido == "Preventivas") ? senalesPreventivas : senalesRestrictivas;

        btnAnterior.onClick.AddListener(() => CambiarSenal(-1));
        btnSiguiente.onClick.AddListener(() => CambiarSenal(1));
        btnFinalizarLeccion.onClick.AddListener(OnFinalizarLeccion); // Nuevo listener

        // Configuración inicial del botón
        btnFinalizarLeccion.gameObject.SetActive(false);
        btnFinalizarLeccion.interactable = false;

        MostrarSenalActual();
    }

    void MostrarSenalActual()
    {
        if (senalesActuales.Length == 0) return;

        SenalSO senal = senalesActuales[indiceActual];
        textoNombre.text = senal.nombre;
        textoDescripcion.text = senal.descripcion;
        imagenSenal.sprite = senal.imagen;

        if (textoProgreso != null)
            textoProgreso.text = $"{indiceActual + 1}/{senalesActuales.Length}";

        // Activar/desactivar botones de navegación
        btnAnterior.interactable = (indiceActual > 0);
        btnSiguiente.interactable = (indiceActual < senalesActuales.Length - 1);

        // Mostrar "Finalizar Lección" solo en la última señal
        bool esUltimaSenal = (indiceActual == senalesActuales.Length - 1);
        btnFinalizarLeccion.gameObject.SetActive(esUltimaSenal);
        btnFinalizarLeccion.interactable = esUltimaSenal;
    }

    void CambiarSenal(int cambio)
    {
        indiceActual = Mathf.Clamp(indiceActual + cambio, 0, senalesActuales.Length - 1);
        MostrarSenalActual();
    }

    // Nuevo método para finalizar lección
    void OnFinalizarLeccion()
    {
        string tipoContenido = PlayerPrefs.GetString("TipoContenido");

        // Actualizar progreso según el tipo de lección
        if (tipoContenido == "Preventivas")
        {
            PlayerPrefs.SetInt("LeccionCompletada", 1); // Desbloquea Restrictivas
        }
        else if (tipoContenido == "Restrictivas")
        {
            PlayerPrefs.SetInt("LeccionCompletada", 2); // Desbloquea Informativas
        }
        // Agrega más condiciones si tienes otros tipos

        PlayerPrefs.Save();

        // Opcional: Efecto visual antes de cambiar de escena
        StartCoroutine(RegresarAlMenuConRetraso(0.5f));
    }

    System.Collections.IEnumerator RegresarAlMenuConRetraso(float delay)
    {
        // Ejemplo: Cambiar color del botón para confirmación visual
        btnFinalizarLeccion.image.color = Color.green;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("SMenu"); // O "Escena_Menu" si prefieres
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Escena_Menu");
    }
}