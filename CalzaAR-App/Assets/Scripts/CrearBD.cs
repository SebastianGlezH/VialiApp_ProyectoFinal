using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using SQLite4Unity3d;

public class CrearBD : MonoBehaviour
{
    public static event Action<SQLiteConnection> OnDatabaseReady;

    void Start()
    {
        string dbName = "gamificacion.db";
        string dbPath = Path.Combine(Application.persistentDataPath, dbName);

        if (!File.Exists(dbPath))
        {
            Debug.Log("Copiando base de datos desde StreamingAssets...");
            StartCoroutine(CopyDatabase(dbName, dbPath));
        }
        else
        {
            Debug.Log("Base ya existe en: " + dbPath);
            InitializeDatabase(dbPath);
        }
    }

    private IEnumerator CopyDatabase(string dbName, string dbPath)
    {
        string sourcePath = Path.Combine(Application.streamingAssetsPath, dbName);

#if UNITY_ANDROID && !UNITY_EDITOR
        UnityWebRequest www = UnityWebRequest.Get(sourcePath);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            File.WriteAllBytes(dbPath, www.downloadHandler.data);
            Debug.Log("Base copiada correctamente a: " + dbPath);
            InitializeDatabase(dbPath);
        }
        else
        {
            Debug.LogError("Error copiando la base: " + www.error);
        }
#else
        File.Copy(sourcePath, dbPath, true);
        Debug.Log("Base copiada correctamente a: " + dbPath);
        InitializeDatabase(dbPath);
#endif
        yield return null;
    }

    private void InitializeDatabase(string dbPath)
    {
        try
        {
            SQLiteConnection db = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            Debug.Log("Base de datos inicializada.");

            if (OnDatabaseReady != null)
            {
                OnDatabaseReady(db);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error al inicializar la base de datos: " + ex.Message);
        }
    }
}