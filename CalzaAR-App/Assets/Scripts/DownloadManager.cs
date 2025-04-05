using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;  // Si usas TextMeshPro
using UnityEngine.Networking; // Para descargar archivos en Android e iOS

public class DownloadManager : MonoBehaviour
{
    public string fileName = "manual_manejo.pdf"; // Nombre del archivo en StreamingAssets
    public TextMeshProUGUI mensajeTexto; // Asigna un TextMeshPro en el inspector (o usa un UI Text)

    public void DescargarPDF()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        string destinationPath = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(destinationPath))
        {
            MostrarMensaje("📄 Archivo ya descargado en: " + destinationPath);
            return;
        }

        StartCoroutine(CopiarArchivo(filePath, destinationPath));
    }

    private System.Collections.IEnumerator CopiarArchivo(string origen, string destino)
    {
        MostrarMensaje("⏳ Descargando archivo...");

        if (origen.Contains("://")) // Para Android e iOS
        {
            using (UnityWebRequest www = UnityWebRequest.Get(origen))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    File.WriteAllBytes(destino, www.downloadHandler.data);
                    MostrarMensaje("✅ Descarga completa: " + destino);
                }
                else
                {
                    MostrarMensaje("❌ Error al descargar: " + www.error);
                }
            }
        }
        else
        {
            File.Copy(origen, destino);
            MostrarMensaje("✅ Archivo guardado en: " + destino);
        }
    }

    private void MostrarMensaje(string mensaje)
    {
        Debug.Log(mensaje);
        if (mensajeTexto != null)
        {
            mensajeTexto.text = mensaje;
        }
    }
}
