using System.IO;
using UnityEngine;
using SQLite4Unity3d;

public class CrearBD : MonoBehaviour
{
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
    }
}
