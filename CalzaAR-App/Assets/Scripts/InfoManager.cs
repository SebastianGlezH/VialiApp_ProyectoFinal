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
    public SenalSO[] senalesInformativas;

    private SenalSO[] senalesActuales;
    private int indiceActual = 0;

    void Start()
    {
        string tipoContenido = PlayerPrefs.GetString("TipoContenido");

        senalesActuales = tipoContenido switch
        {
            "Preventivas" => senalesPreventivas,
            "Restrictivas" => senalesRestrictivas,
            "Informativas" => senalesInformativas,
            _ => senalesPreventivas
        };

        if (btnAnterior != null) btnAnterior.onClick.AddListener(() => CambiarSenal(-1));
        if (btnSiguiente != null) btnSiguiente.onClick.AddListener(() => CambiarSenal(1));
        if (btnFinalizarLeccion != null) btnFinalizarLeccion.onClick.AddListener(OnFinalizarLeccion);

        if (btnFinalizarLeccion != null)
        {
            btnFinalizarLeccion.gameObject.SetActive(false);
            btnFinalizarLeccion.interactable = false;
        }

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

        if (btnAnterior != null) btnAnterior.interactable = (indiceActual > 0);
        if (btnSiguiente != null) btnSiguiente.interactable = (indiceActual < senalesActuales.Length - 1);

        bool esUltimaSenal = (indiceActual == senalesActuales.Length - 1);
        if (btnFinalizarLeccion != null)
        {
            btnFinalizarLeccion.gameObject.SetActive(esUltimaSenal);
            btnFinalizarLeccion.interactable = esUltimaSenal;
        }
    }

    void CambiarSenal(int cambio)
    {
        indiceActual = Mathf.Clamp(indiceActual + cambio, 0, senalesActuales.Length - 1);
        MostrarSenalActual();
    }

    void OnFinalizarLeccion()
    {
        string tipoContenido = PlayerPrefs.GetString("TipoContenido");
        int nuevoNivel = 0;

        if (tipoContenido == "Preventivas") nuevoNivel = 2;
        else if (tipoContenido == "Restrictivas") nuevoNivel = 3;
        else if (tipoContenido == "Informativas") nuevoNivel = 4;

        if (DataManager.loggedInUser != null && nuevoNivel > DataManager.loggedInUser.nivel)
        {
            DataManager.loggedInUser.nivel = nuevoNivel;
            // Llamamos al nuevo método para guardar los cambios en la base de datos.
            DataManager.SaveUserChanges();
        }

        if (btnFinalizarLeccion != null)
        {
            btnFinalizarLeccion.image.color = Color.green;
            Invoke("RegresarAlMenu", 0.5f);
        }
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
