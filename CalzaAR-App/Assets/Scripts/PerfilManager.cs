using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PerfilManager : MonoBehaviour
{
    public Button backButton;
    public Button logoutButton;

    private bool loggedIn = false; // Mantener el estado de inicio de sesión en esta clase

    private void Start()
    {
        backButton.onClick.AddListener(BackToPreviousScene);
        logoutButton.onClick.AddListener(Logout);

        // Verificar el estado de inicio de sesión solo si aún no se ha verificado en esta instancia
        if (!loggedIn)
        {
            CheckLoginStatus();
        }
    }

    // Este método se llama cuando se vuelve a la escena del perfil
    public void OnEnable()
    {
        // Verificar el estado de inicio de sesión si no está logeado
        if (!loggedIn)
        {
            CheckLoginStatus();
        }
    }

    public void CheckLoginStatus()
    {
        if (!SessionManager.Instance.IsUserLoggedIn())
        {
            // Si el usuario no ha iniciado sesión, volver a la escena de inicio de sesión
            SceneManager.LoadScene("Perfil");
        }
        else
        {
            // Actualizar el estado de inicio de sesión en esta clase
            loggedIn = false;
        }
    }

    public void BackToPreviousScene()
    {
        // Implementa la lógica para volver a la escena anterior según tus necesidades
    }

    public void Logout()
    {
        // Llama al método de cierre de sesión del SessionManager
        SessionManager.Instance.Logout();
        // Actualizar el estado de inicio de sesión en esta clase
        loggedIn = false;
    }
}
