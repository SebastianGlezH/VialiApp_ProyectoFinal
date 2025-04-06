using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Salir()
    {
        Debug.Log("Saliendo de la aplicación...");
        Application.Quit();

        // Si estás en el editor de Unity, usa esto para simular la salida
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}