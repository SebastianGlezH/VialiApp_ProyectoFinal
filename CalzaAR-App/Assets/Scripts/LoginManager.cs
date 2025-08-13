using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using SQLite4Unity3d;
using System;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField inputUsuario;
    public TMP_InputField inputPassword;
    public TMP_InputField inputUsuarioRegistro;
    public TMP_InputField inputPasswordRegistro;
    public TMP_InputField inputConfirmarPassword;
    public GameObject canvasLogin;
    public GameObject canvasRegistro;
    public GameObject imLoading;

    private SQLiteConnection db;

    void OnEnable()
    {
        CrearBD.OnDatabaseReady += OnDatabaseReady;
    }

    void OnDisable()
    {
        CrearBD.OnDatabaseReady -= OnDatabaseReady;
    }

    private void OnDatabaseReady(SQLiteConnection connection)
    {
        this.db = connection;
        try
        {
            this.db.CreateTable<Usuario>();
            Debug.Log("Tabla de usuarios creada.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error al crear la tabla de usuarios: " + ex.Message);
        }

        if (imLoading != null)
        {
            imLoading.SetActive(false);
        }
    }

    public void IniciarSesion()
    {
        if (db == null)
        {
            Debug.LogError("Error: La conexión a la base de datos no está inicializada.");
            return;
        }

        if (imLoading != null) imLoading.SetActive(true);
        string nombreUsuario = inputUsuario.text.Trim();
        string contrasena = inputPassword.text.Trim();

        var usuarioEncontrado = db.Table<Usuario>().Where(u => u.usuario == nombreUsuario && u.pass == contrasena).FirstOrDefault();

        if (usuarioEncontrado != null)
        {
            Debug.Log("Inicio de sesión exitoso.");
            DataManager.loggedInUser = usuarioEncontrado;
            SceneManager.LoadScene("Inicio");
        }
        else
        {
            Debug.Log("Usuario o contraseña incorrectos.");
            if (imLoading != null) imLoading.SetActive(false);
        }
    }

    public void RegistrarUsuario()
    {
        if (db == null)
        {
            Debug.LogError("Error: La conexión a la base de datos no está inicializada.");
            return;
        }

        if (imLoading != null) imLoading.SetActive(true);
        string nombreUsuario = inputUsuarioRegistro.text.Trim();
        string contrasena = inputPasswordRegistro.text.Trim();
        string confirmarContrasena = inputConfirmarPassword.text.Trim();

        if (contrasena != confirmarContrasena)
        {
            Debug.Log("Las contraseñas no coinciden.");
            if (imLoading != null) imLoading.SetActive(false);
            return;
        }

        var usuarioExistente = db.Table<Usuario>().Where(u => u.usuario == nombreUsuario).FirstOrDefault();
        if (usuarioExistente != null)
        {
            Debug.Log("Este nombre de usuario ya está registrado.");
            if (imLoading != null) imLoading.SetActive(false);
            return;
        }

        Usuario nuevo = new Usuario
        {
            usuario = nombreUsuario,
            pass = contrasena,
            nivel = 1 // Nuevo usuario comienza en nivel 1
        };

        db.Insert(nuevo);
        DataManager.loggedInUser = nuevo;
        Debug.Log("Usuario registrado correctamente.");

        if (canvasRegistro != null) canvasRegistro.SetActive(false);
        if (canvasLogin != null) canvasLogin.SetActive(true);
        if (imLoading != null) imLoading.SetActive(false);
    }

    public void MostrarRegistro()
    {
        if (canvasLogin != null) canvasLogin.SetActive(false);
        if (canvasRegistro != null) canvasRegistro.SetActive(true);
    }

    public void MostrarLogin()
    {
        if (canvasRegistro != null) canvasRegistro.SetActive(false);
        if (canvasLogin != null) canvasLogin.SetActive(true);
    }
}