using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour
{
    public GameObject panelTestPreventivas;
    public GameObject panelTestRestrictivas;

    void Start()
    {
        string tipoContenido = PlayerPrefs.GetString("TipoContenido");

        // Activa solo el panel del test seleccionado
        panelTestPreventivas.SetActive(tipoContenido == "Preventivas");
        panelTestRestrictivas.SetActive(tipoContenido == "Restrictivas");
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Escena_Menu");
    }
}
