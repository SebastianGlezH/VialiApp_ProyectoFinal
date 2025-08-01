using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Login : MonoBehaviour
{
    public Servidor servidor;
    public TMP_InputField inpUsuario;
    public TMP_InputField inpPass;
    public GameObject imLoading;
    public DBUsuario usuario;
    public void IniciarSesion()
    {
        StartCoroutine(Iniciar());
    }

    IEnumerator Iniciar()
    {
        imLoading.SetActive(true);
        string[] datos = new string[2];
        datos[0] = inpUsuario.text;
        datos[1] = inpPass.text;

        StartCoroutine(servidor.ConsumirServicio("login", datos, PosCargar));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
        imLoading.SetActive(false);
    }


    void PosCargar()
    {
        switch (servidor.respuesta.codigo)
        {
            case 204: //El usuario o la contraseña son incorrectos
                print("El usuario o la contraseña son incorrectos");
                break;
            case 205: // Inicio de sesión correcto
                SceneManager.LoadScene("Inicio");
                usuario = JsonUtility.FromJson<DBUsuario>(servidor.respuesta.respuesta);
                break;
            case 402: // 402: Faltan datos para ejecutar la accion solicitada
                print(servidor.respuesta.mensaje);
                break;
            case 404: // error
                print("Error, no se puede conectar con el servidor");
                break;
            default:
                break;
        }
    }
}
