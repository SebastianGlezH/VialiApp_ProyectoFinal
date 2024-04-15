using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance;

    public void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CheckLoginStatus()
    {
        if (IsUserLoggedIn())
        {
            SceneManager.LoadScene("Perfil");
        }
        else
        {
            SceneManager.LoadScene("Usuario_Login");
        }
    }

    public void Logout()
    {
        PlayerPrefs.DeleteKey("LoggedIn");
        SceneManager.LoadScene("Usuario_Login");
    }

    public bool IsUserLoggedIn()
    {
        return PlayerPrefs.HasKey("LoggedIn") && PlayerPrefs.GetInt("LoggedIn") == 1;
    }
}
