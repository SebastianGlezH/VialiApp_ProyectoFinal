using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EscenaResultados : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI textoResultado;
    public Button botonRegresar;

    void Start()
    {
        // Obtener resultados guardados
        int aciertos = PlayerPrefs.GetInt("Aciertos", 0);
        int totalPreguntas = PlayerPrefs.GetInt("TotalPreguntas", 1);

        // Calcular porcentaje
        float porcentaje = (float)aciertos / totalPreguntas * 100;

        // Mostrar resultados
        textoResultado.text = $"{aciertos}/{totalPreguntas} correctas\n({porcentaje:F0}% de aciertos)";

        // Configurar botón
        botonRegresar.onClick.AddListener(() => {
            SceneManager.LoadScene("Inicio");
        });
    }
}