using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button registerButton;
    public Button goToLoginButton;

    public GameObject emptyFieldErrorObject; // Objeto para mostrar error de campos vacíos
    public GameObject usernameExistsErrorObject; // Objeto para mostrar error de nombre de usuario existente

    ArrayList credentials;

    // Start is called before the first frame update
    void Start()
    {
        registerButton.onClick.AddListener(writeStuffToFile);
        goToLoginButton.onClick.AddListener(goToLoginScene);

        if (File.Exists(Application.persistentDataPath + "/credentials.txt"))
        {
            credentials = new ArrayList(File.ReadAllLines(Application.persistentDataPath + "/credentials.txt"));
        }
        else
        {
            File.WriteAllText(Application.persistentDataPath + "/credentials.txt", "");
        }
    }

    void goToLoginScene()
    {
        SceneManager.LoadScene("Usuario_Login");
    }

    void writeStuffToFile()
    {
        if (AreInputsEmpty())
        {
            emptyFieldErrorObject.SetActive(true); // Activar el objeto de error de campos vacíos
            return;
        }

        if (PlayerPrefs.HasKey(usernameInput.text))
        {
            usernameExistsErrorObject.SetActive(true); // Activar el objeto de error de nombre de usuario existente
            Debug.Log($"Username '{usernameInput.text}' already exists");
            return;
        }

        // Guardar las credenciales en PlayerPrefs
        PlayerPrefs.SetString(usernameInput.text, passwordInput.text);
        PlayerPrefs.Save(); // Guardar los cambios

        Debug.Log("Account Registered");
    }

    private bool AreInputsEmpty()
    {
        if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text))
        {
            Debug.Log("Please fill in all fields.");
            return true;
        }
        else
        {
            return false;
        }
    }
}
