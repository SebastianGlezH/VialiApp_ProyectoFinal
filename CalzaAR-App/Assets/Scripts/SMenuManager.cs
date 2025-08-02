using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SMenuManager : MonoBehaviour
{
    [Header("Botones")]
    public Button btnPreventivas;
    public Button btnRestrictivas;
    public Button btnInformativas;
    public Button btnTuristicas;

    [Header("Colores")]
    public Color colorBloqueado = Color.gray;  // Color cuando el bot�n est� bloqueado
    public Color colorActivo = Color.white;    // Color normal (puedes ajustarlo en el Inspector)

    void Start()
    {
        // Inicializar los botones seg�n el progreso guardado
        ActualizarBotones();
    }

    // M�todo principal para actualizar el estado de los botones
    private void ActualizarBotones()
    {
        int leccionCompletada = PlayerPrefs.GetInt("LeccionCompletada", 0);

        // Preventivas siempre est� activa
        btnPreventivas.interactable = true;
        btnPreventivas.image.color = colorActivo;

        // Restrictivas
        btnRestrictivas.interactable = (leccionCompletada >= 1);
        btnRestrictivas.image.color = btnRestrictivas.interactable ? colorActivo : colorBloqueado;

        // Informativas
        btnInformativas.interactable = (leccionCompletada >= 2);
        btnInformativas.image.color = btnInformativas.interactable ? colorActivo : colorBloqueado;

        // Tur�sticas
        btnTuristicas.interactable = (leccionCompletada >= 3);
        btnTuristicas.image.color = btnTuristicas.interactable ? colorActivo : colorBloqueado;
    }

    // M�todo para llamar cuando se completa una lecci�n
    public void CompletarLeccion(int leccionIndex)
    {
        PlayerPrefs.SetInt("LeccionCompletada", leccionIndex);
        PlayerPrefs.Save(); // Guardar cambios

        // Actualizar botones (opcional: recargar la escena si prefieres)
        ActualizarBotones();
    }

    // --- Ejemplos de c�mo llamar a CompletarLeccion ---
    // Desde otros scripts o eventos de Unity (ej: al finalizar una lecci�n):

    public void OnLeccionPreventivasCompletada()
    {
        CompletarLeccion(1); // Desbloquea "Restrictivas"
    }

    public void OnLeccionRestrictivasCompletada()
    {
        CompletarLeccion(2); // Desbloquea "Informativas"
    }

    public void OnLeccionInformativasCompletada()
    {
        CompletarLeccion(3); // Desbloquea "Tur�sticas"
    }
}