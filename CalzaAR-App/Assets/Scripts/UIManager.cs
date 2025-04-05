using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public void Salir()
    {
        Debug.Log("Saliendo de la aplicación...");
        Application.Quit();

        // Si estás en el editor de Unity, usa esto para simular la salida
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void MostrarMensaje()
    {
        Debug.Log("Mostrando un mensaje...");
    }

    public void CambiarEscena(string nombreEscena)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nombreEscena);
    }
}
