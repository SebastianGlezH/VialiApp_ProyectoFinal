using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using SQLite4Unity3d;

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

    void Start()
    {
        string dbName = "gamificacion.db";
        string dbPath = Path.Combine(Application.persistentDataPath, dbName);

        if (!File.Exists(dbPath))
        {
            Debug.Log("Copiando base de datos desde StreamingAssets...");

            string sourcePath = Path.Combine(Application.streamingAssetsPath, dbName);

#if UNITY_ANDROID && !UNITY_EDITOR
            UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(sourcePath);
            www.SendWebRequest();
            while (!www.isDone) { }
            if (string.IsNullOrEmpty(www.error))
            {
                File.WriteAllBytes(dbPath, www.downloadHandler.data);
                Debug.Log("Base copiada correctamente a: " + dbPath);
            }
            else
            {
                Debug.LogError("Error copiando la base: " + www.error);
            }
#else
            File.Copy(sourcePath, dbPath);
            Debug.Log("Base copiada correctamente a: " + dbPath);
#endif
        }
        else
        {
            Debug.Log("Base ya existe en: " + dbPath);
        }

        db = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        db.CreateTable<Usuario>();

        Debug.Log("Base de datos lista en: " + dbPath);
    }

    public void IniciarSesion()
    {
        imLoading.SetActive(true);
        string nombreUsuario = inputUsuario.text.Trim();
        string contrasena = inputPassword.text.Trim();

        var usuarioEncontrado = db.Table<Usuario>().Where(u => u.usuario == nombreUsuario && u.pass == contrasena).FirstOrDefault();

        if (usuarioEncontrado != null)
        {
            Debug.Log("Inicio de sesión exitoso.");
            SceneManager.LoadScene("Inicio");
        }
        else
        {
            Debug.Log("Usuario o contraseña incorrectos.");
            imLoading.SetActive(false);
        }

        imLoading.SetActive(false);
    }

    public void RegistrarUsuario()
    {
        imLoading.SetActive(true);

        string nombreUsuario = inputUsuarioRegistro.text.Trim();
        string contrasena = inputPasswordRegistro.text.Trim();
        string confirmarContrasena = inputConfirmarPassword.text.Trim();

        if (contrasena != confirmarContrasena)
        {
            Debug.Log("Las contraseñas no coinciden.");
            imLoading.SetActive(false);
            return;
        }

        var usuarioExistente = db.Table<Usuario>().Where(u => u.usuario == nombreUsuario).FirstOrDefault();

        if (usuarioExistente != null)
        {
            Debug.Log("Este nombre de usuario ya está registrado.");
            imLoading.SetActive(false);
            return;
        }

        Usuario nuevo = new Usuario
        {
            usuario = nombreUsuario,
            pass = contrasena,
            nivel = 1
        };

        db.Insert(nuevo);
        Debug.Log("Usuario registrado correctamente.");

        canvasRegistro.SetActive(false);
        canvasLogin.SetActive(true);
        imLoading.SetActive(false);
    }

    public void MostrarRegistro()
    {
        canvasLogin.SetActive(false);
        canvasRegistro.SetActive(true);
    }

    public void MostrarLogin()
    {
        canvasRegistro.SetActive(false);
        canvasLogin.SetActive(true);
    }
}

[System.Serializable]
public class Usuario
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    public string usuario { get; set; }
    public string pass { get; set; }
    public int nivel { get; set; }
}
