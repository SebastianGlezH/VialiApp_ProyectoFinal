using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SMenuManager : MonoBehaviour
{
    // Las referencias de los botones se asignan en el Inspector.
    [Header("Botones")]
    public Button btnPreventivas;
    public Button btnRestrictivas;
    public Button btnInformativas;
    public Button btnTestGeneral;
    public Button btnVolver;

    [Header("Colores")]
    public Color colorBloqueado = Color.gray;
    public Color colorActivo = Color.white;

    void Start()
    {
        // Se configura la UI y se actualizan los botones cada vez que la escena se carga.
        if (btnPreventivas != null) btnPreventivas.onClick.AddListener(() => CargarInformacion("Preventivas"));
        if (btnRestrictivas != null) btnRestrictivas.onClick.AddListener(() => CargarInformacion("Restrictivas"));
        if (btnInformativas != null) btnInformativas.onClick.AddListener(() => CargarInformacion("Informativas"));
        if (btnTestGeneral != null) btnTestGeneral.onClick.AddListener(() => CargarTestGeneral());
        if (btnVolver != null) btnVolver.onClick.AddListener(VolverAMenuPrincipal);

        ActualizarBotones();
    }

    public void ActualizarBotones()
    {
        // Si no hay un usuario logueado, no hacemos nada.
        if (DataManager.loggedInUser == null) return;

        // Obtenemos el nivel directamente del usuario en memoria.
        int nivelUsuario = DataManager.loggedInUser.nivel;

        // Lógica de bloqueo de botones basada en el nivel del usuario.
        if (btnPreventivas != null)
        {
            btnPreventivas.interactable = (nivelUsuario >= 1);
            btnPreventivas.image.color = btnPreventivas.interactable ? colorActivo : colorBloqueado;
        }

        if (btnRestrictivas != null)
        {
            btnRestrictivas.interactable = (nivelUsuario >= 2);
            btnRestrictivas.image.color = btnRestrictivas.interactable ? colorActivo : colorBloqueado;
        }

        if (btnInformativas != null)
        {
            btnInformativas.interactable = (nivelUsuario >= 3);
            btnInformativas.image.color = btnInformativas.interactable ? colorActivo : colorBloqueado;
        }

        if (btnTestGeneral != null)
        {
            // El Test General se activa cuando el nivel es 4
            btnTestGeneral.gameObject.SetActive(nivelUsuario >= 4);
        }
    }

    public void CargarInformacion(string tipoContenido)
    {
        PlayerPrefs.SetString("TipoContenido", tipoContenido);
        SceneManager.LoadScene("Escena_Informacion");
    }

    public void CargarTestGeneral()
    {
        PlayerPrefs.SetString("TipoContenido", "TestGeneral");
        SceneManager.LoadScene("Escena_Tests");
    }

    public void VolverAMenuPrincipal()
    {
        SceneManager.LoadScene("Escena_Menu");
    }
}
