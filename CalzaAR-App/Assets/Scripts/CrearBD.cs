using System.IO;
using UnityEngine;
using SQLite4Unity3d;

public class CrearBD : MonoBehaviour
{
    private SQLiteConnection db;

    void Start()
    {
        string dbPath = Path.Combine(Application.persistentDataPath, "gamificacion.db");
        Debug.Log("Ruta DB: " + dbPath);

        db = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        // Crea la tabla solo si no existe
        db.CreateTable<Usuario>();

        // Opcional: Inserta un usuario de prueba solo si no existe ninguno
        if (db.Table<Usuario>().Count() == 0)
        {
            var usuario = new Usuario
            {
                usuario = "diego",
                pass = "1234",
                nivel = 1
            };
            db.Insert(usuario);
            Debug.Log("Usuario inicial insertado");
        }
    }
}
