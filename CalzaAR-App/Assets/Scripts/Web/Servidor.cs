using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "Servidor", menuName = "Vialiapp/Servidor", order = 1)]
public class Servidor : ScriptableObject
{
    public string servidor;
    public Servicio[] servicios;

    public bool ocupado = false;
    public Respuesta respuesta;


    public IEnumerator ConsumirServicio(string nombre, string[] datos, UnityAction e)
    {
        ocupado = true;
        WWWForm formulario = new WWWForm();
        Servicio s = new Servicio();
        for (int i = 0; i < servicios.Length; i++)
        {
            if (servicios[i].nombre.Equals(nombre))
            {
                s = servicios[i];
            }
        }
        for (int i = 0; i < s.parametros.Length; i++)
        {
            formulario.AddField(s.parametros[i], datos[i]);
        }

        UnityWebRequest www = UnityWebRequest.Post(servidor + "/" + s.URL, formulario);
        Debug.Log(servidor + "/" + s.URL);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            respuesta = new Respuesta();
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            respuesta = JsonUtility.FromJson<Respuesta>(www.downloadHandler.text);
            respuesta.LimpiarRespuesta();
        }

        ocupado = false;
        e.Invoke();
    }
}

[System.Serializable]

public class Servicio
{
    public string nombre;
    public string URL;
    public string[] parametros;
}

[System.Serializable]

public class Respuesta
{
    public int codigo;
    public string mensaje;
    public string respuesta;

    public void LimpiarRespuesta()
    {
        respuesta = respuesta.Replace('#', '"');
    }

    public Respuesta()
    {
        codigo = 404;
        mensaje = "Error";
    }
}

[System.Serializable]

public class DBUsuario
{
    public int      id;
    public string   usuario;
    public string   pass;
    public string   jugador;
    public string   nivel;
}