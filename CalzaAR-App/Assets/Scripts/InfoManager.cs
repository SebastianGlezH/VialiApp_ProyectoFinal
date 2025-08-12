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
    public Button btnFinalizarLeccion;

    [Header("Datos de Señales")]
    public SenalSO[] senalesPreventivas;
    public SenalSO[] senalesRestrictivas;
    public SenalSO[] senalesInformativas; // Nuevo array para señales informativas

    private SenalSO[] senalesActuales;
    private int indiceActual = 0;

    void Start()
    {
        // Cargar el tipo de contenido seleccionado
        string tipoContenido = PlayerPrefs.GetString("TipoContenido");

        // Asignar el conjunto de señales correspondiente
        senalesActuales = tipoContenido switch
        {
            "Preventivas" => senalesPreventivas,
            "Restrictivas" => senalesRestrictivas,
            "Informativas" => senalesInformativas,
            _ => senalesPreventivas
        };

        // Configurar listeners
        btnAnterior.onClick.AddListener(() => CambiarSenal(-1));
        btnSiguiente.onClick.AddListener(() => CambiarSenal(1));
        btnFinalizarLeccion.onClick.AddListener(OnFinalizarLeccion);

        // Configurar botón de finalizar
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

        // Mostrar progreso (ej: "1/5")
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

    void OnFinalizarLeccion()
    {
        string tipoContenido = PlayerPrefs.GetString("TipoContenido");

        // Actualizar progreso según el tipo de lección completada
        if (tipoContenido == "Preventivas")
        {
            FindObjectOfType<SMenuManager>().OnLeccionPreventivasCompletada();
        }
        else if (tipoContenido == "Restrictivas")
        {
            FindObjectOfType<SMenuManager>().OnLeccionRestrictivasCompletada();
        }
        // No necesitamos caso para Informativas pues es el último nivel

        // Opcional: Efecto visual de confirmación
        btnFinalizarLeccion.image.color = Color.green;
        Invoke("RegresarAlMenu", 0.5f);
    }

    void RegresarAlMenu()
    {
        SceneManager.LoadScene("SMenu");
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Escena_Menu");
    }
}