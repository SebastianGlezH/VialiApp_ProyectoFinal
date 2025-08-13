using SQLite4Unity3d;

// La clase DataManager ahora es est�tica y no necesita ser un MonoBehaviour.
public static class DataManager
{
    // Almacena la conexi�n a la base de datos.
    public static SQLiteConnection db;
    // Almacena el usuario que ha iniciado sesi�n.
    public static Usuario loggedInUser;

    // M�todo para guardar los cambios del usuario en la base de datos
    public static void SaveUserChanges()
    {
        if (loggedInUser != null && db != null)
        {
            db.Update(loggedInUser);
        }
    }
}

// La clase Usuario se mantiene en este archivo para una mejor organizaci�n.
[System.Serializable]
public class Usuario
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    public string usuario { get; set; }
    public string pass { get; set; }
    public int nivel { get; set; }
}
