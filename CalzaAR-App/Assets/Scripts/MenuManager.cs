using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Botones asignables desde el Inspector
    public Button btnTestPreventivas;
    public Button btnTestRestrictivas;
    public Button btnInfoPreventivas;
    public Button btnInfoRestrictivas;
    public Button btnSalir; // Nuevo bot�n de salir

    void Start()
    {
        // Configuraci�n de listeners para todos los botones
        btnTestPreventivas.onClick.AddListener(() => CargarTest("Preventivas"));
        btnTestRestrictivas.onClick.AddListener(() => CargarTest("Restrictivas"));
        btnInfoPreventivas.onClick.AddListener(() => CargarInfo("Preventivas"));
        btnInfoRestrictivas.onClick.AddListener(() => CargarInfo("Restrictivas"));
        btnSalir.onClick.AddListener(SalirDelJuego); // Asigna el m�todo de salida
    }

    // M�todos para cambiar de escena
    public void CargarTest(string tipoTest)
    {
        PlayerPrefs.SetString("TipoContenido", tipoTest);
        SceneManager.LoadScene("Escena_Tests");
    }

    public void CargarInfo(string tipoInfo)
    {
        PlayerPrefs.SetString("TipoContenido", tipoInfo);
        SceneManager.LoadScene("Escena_Informacion");
    }

    // Nuevo m�todo para salir de la aplicaci�n
    public void SalirDelJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Cierra en el Editor
#else
            Application.Quit(); // Cierra en la build final
#endif
    }
}