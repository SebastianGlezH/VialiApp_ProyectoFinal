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

        if (File.Exists(Application.dataPath + "/credentials.txt"))
        {
            credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/credentials.txt", "");
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

        bool isExists = false;

        credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        foreach (var i in credentials)
        {
            if (i.ToString().Contains(usernameInput.text))
            {
                isExists = true;
                break;
            }
        }

        if (isExists)
        {
            usernameExistsErrorObject.SetActive(true); // Activar el objeto de error de nombre de usuario existente
            Debug.Log($"Username '{usernameInput.text}' already exists");
        }
        else
        {
            credentials.Add(usernameInput.text + ":" + passwordInput.text);
            File.WriteAllLines(Application.dataPath + "/credentials.txt", (String[])credentials.ToArray(typeof(string)));
            Debug.Log("Account Registered");
        }
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
