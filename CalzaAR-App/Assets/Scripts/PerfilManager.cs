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


        if (!loggedIn)
        {
            CheckLoginStatus();
        }
    }


    public void OnEnable()
    {
        if (!loggedIn)
        {
            CheckLoginStatus();
        }
    }

    public void CheckLoginStatus()
    {
        if (!SessionManager.Instance.IsUserLoggedIn())
        {
            SceneManager.LoadScene("Usuario_Login");
        }
        else
        {
            loggedIn = true;
        }
    }

    public void BackToPreviousScene()
    {
    }

    public void Logout()
    {
        SessionManager.Instance.Logout();
        loggedIn = false;
    }
}
