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

    private string[] credentials;

    private void Start()
    {
        loginButton.onClick.AddListener(LoginAttempt);
        goToRegisterButton.onClick.AddListener(MoveToRegister);

        if (File.Exists(Application.dataPath + "/credentials.txt"))
        {
            // Cargar las credenciales una vez al inicio
            credentials = File.ReadAllLines(Application.dataPath + "/credentials.txt");
        }
        else
        {
            Debug.LogError("Credential file doesn't exist");
        }
    }

    private void LoginAttempt()
    {
        bool credentialsCorrect = false;

        foreach (string credential in credentials)
        {
            string[] parts = credential.Split(':');
            string username = parts[0];
            string password = parts[1];

            if (username == usernameInput.text && password == passwordInput.text)
            {
                credentialsCorrect = true;
                break;
            }
        }

        if (credentialsCorrect)
        {
            Debug.Log($"Logging in as '{usernameInput.text}'");
            LoadWelcomeScreen();
        }
        else
        {
            Debug.Log("Incorrect credentials");
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
