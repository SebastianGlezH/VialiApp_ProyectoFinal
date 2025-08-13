using SQLite4Unity3d;

// La clase DataManager ahora es estática y no necesita ser un MonoBehaviour.
public static class DataManager
{
    // Almacena la conexión a la base de datos.
    public static SQLiteConnection db;
    // Almacena el usuario que ha iniciado sesión.
    public static Usuario loggedInUser;

    // Método para guardar los cambios del usuario en la base de datos
    public static void SaveUserChanges()
    {
        if (loggedInUser != null && db != null)
        {
            db.Update(loggedInUser);
        }
    }
}

// La clase Usuario se mantiene en este archivo para una mejor organización.
[System.Serializable]
public class Usuario
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    public string usuario { get; set; }
    public string pass { get; set; }
    public int nivel { get; set; }
}
