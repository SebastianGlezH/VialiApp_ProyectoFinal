using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Botones del menú principal
    public Button btnSenalizacion; // Botón para ir al menú de señalización
    public Button btnSalir;       // Botón para salir del juego

    void Start()
    {
        // Asigna el listener al botón de Señalización si está asignado
        if (btnSenalizacion != null)
        {
            btnSenalizacion.onClick.AddListener(CargarSMenu);
        }

        // Asigna el listener al botón de Salir si está asignado
        if (btnSalir != null)
        {
            btnSalir.onClick.AddListener(SalirDelJuego);
        }
    }

    public void CargarSMenu()
    {
        SceneManager.LoadScene("SMenu");
    }

    public void SalirDelJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
