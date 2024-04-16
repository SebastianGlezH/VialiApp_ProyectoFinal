using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Login : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;
    public Button goToRegisterButton;

    public GameObject errorObject; // Referencia al objeto que quieres activar

    private string[] credentials;

    private void Start()
    {
        loginButton.onClick.AddListener(LoginAttempt);
        goToRegisterButton.onClick.AddListener(MoveToRegister);

        // No es necesario cargar las credenciales aquí, ya que se cargan al iniciar la aplicación
    }

    private void LoginAttempt()
    {
        string savedPassword = PlayerPrefs.GetString(usernameInput.text);

        if (savedPassword == passwordInput.text)
        {
            Debug.Log($"Logging in as '{usernameInput.text}'");
            LoadWelcomeScreen();
        }
        else
        {
            Debug.Log("Incorrect credentials");
            errorObject.SetActive(true); // Activar el objeto de error
        }
    }

    private void MoveToRegister()
    {
        SceneManager.LoadScene("Usuario_Registrar");
    }

    private void LoadWelcomeScreen()
    {
        SceneManager.LoadScene("Perfil");
    }
}
