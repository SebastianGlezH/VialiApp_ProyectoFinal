using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InfoManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI textoNombre; // Texto para el nombre de la se�al
    public TextMeshProUGUI textoDescripcion; // Texto para la descripci�n
    public Image imagenSe�al; // Imagen de la se�al
    public Button btnAnterior; // Bot�n "Anterior"
    public Button btnSiguiente; // Bot�n "Siguiente"
    public TextMeshProUGUI textoProgreso; // Texto opcional para "1/10" (Opcional)

    [Header("Datos de Se�ales")]
    public Se�alSO[] se�alesPreventivas; // Lista de se�ales preventivas
    public Se�alSO[] se�alesRestrictivas; // Lista de se�ales restrictivas

    private Se�alSO[] se�alesActuales; // Se�ales que se est�n mostrando actualmente
    private int indiceActual = 0; // �ndice de la se�al actual

    void Start()
    {
        // Cargar el tipo de contenido seleccionado (Preventivas/Restrictivas)
        string tipoContenido = PlayerPrefs.GetString("TipoContenido");

        // Asignar el conjunto de se�ales correspondiente
        se�alesActuales = (tipoContenido == "Preventivas") ? se�alesPreventivas : se�alesRestrictivas;

        // Configurar listeners de los botones
        btnAnterior.onClick.AddListener(() => CambiarSe�al(-1));
        btnSiguiente.onClick.AddListener(() => CambiarSe�al(1));

        // Mostrar la primera se�al
        MostrarSe�alActual();
    }

    void MostrarSe�alActual()
    {
        if (se�alesActuales.Length == 0) return;

        // Obtener la se�al actual
        Se�alSO se�al = se�alesActuales[indiceActual];

        // Actualizar UI
        textoNombre.text = se�al.nombre;
        textoDescripcion.text = se�al.descripcion;
        imagenSe�al.sprite = se�al.imagen;

        // Actualizar progreso (opcional)
        if (textoProgreso != null)
            textoProgreso.text = $"{indiceActual + 1}/{se�alesActuales.Length}";

        // Actualizar estado de los botones
        btnAnterior.interactable = (indiceActual > 0);
        btnSiguiente.interactable = (indiceActual < se�alesActuales.Length - 1);
    }

    void CambiarSe�al(int cambio)
    {
        // Cambiar el �ndice y asegurarse de que est� dentro de los l�mites
        indiceActual = Mathf.Clamp(indiceActual + cambio, 0, se�alesActuales.Length - 1);

        // Mostrar la nueva se�al
        MostrarSe�alActual();
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Escena_Menu");
    }
}