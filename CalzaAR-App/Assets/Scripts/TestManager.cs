using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour
{
    [Header("Componentes UI")]
    public Image imagenPregunta;
    public TextMeshProUGUI textoPregunta;
    public Button[] botonesOpciones;
    public TextMeshProUGUI textoProgreso;
    public Button btnSalir; // Nuevo botón para salir

    [Header("Configuración Visual")]
    public Color colorCorrecto = Color.green;
    public Color colorIncorrecto = Color.red;
    public Color colorNormal = Color.white;
    public float tiempoFeedback = 1f;

    [Header("Gestión de Preguntas")]
    public List<PreguntaSO> todasLasPreguntas;
    public string escenaResultados = "Escena_Resultados";

    [Header("Sonidos")]
    public AudioClip sonidoCorrecto;
    public AudioClip sonidoIncorrecto;
    private AudioSource audioSource;

    private List<PreguntaSO> preguntasAleatorias;
    private int preguntaActual = 0;
    private int aciertos = 0;
    private bool esperandoRespuesta = true;

    void Start()
    {
        // Configuramos el botón de salir.
        if (btnSalir != null)
        {
            btnSalir.onClick.AddListener(SalirAlMenu);
        }

        InicializarTest();

        // Configurar AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void InicializarTest()
    {
        if (!ValidarComponentes()) return;

        aciertos = 0;
        preguntaActual = 0;
        // Barajamos la lista de preguntas y tomamos solo las primeras 10.
        preguntasAleatorias = todasLasPreguntas.OrderBy(p => Random.value).Take(10).ToList();

        ConfigurarBotones();
        MostrarPregunta();
    }

    bool ValidarComponentes()
    {
        if (imagenPregunta == null) Debug.LogError("Falta asignar 'imagenPregunta'");
        if (textoPregunta == null) Debug.LogError("Falta asignar 'textoPregunta'");
        if (botonesOpciones.Length < 2) Debug.LogError("Se necesitan al menos 2 botones");
        if (todasLasPreguntas.Count < 10) Debug.LogError("Se necesitan al menos 10 preguntas para el test.");

        return imagenPregunta != null &&
               textoPregunta != null &&
               botonesOpciones.Length >= 2 &&
               todasLasPreguntas.Count >= 10;
    }

    void ConfigurarBotones()
    {
        for (int i = 0; i < botonesOpciones.Length; i++)
        {
            int index = i;
            botonesOpciones[i].onClick.RemoveAllListeners();
            botonesOpciones[i].onClick.AddListener(() => Responder(index));
        }
    }

    void MostrarPregunta()
    {
        if (preguntaActual >= preguntasAleatorias.Count)
        {
            FinalizarTest();
            return;
        }

        PreguntaSO pregunta = preguntasAleatorias[preguntaActual];

        imagenPregunta.sprite = pregunta.imagenSenal;
        textoPregunta.text = pregunta.pregunta;
        textoProgreso.text = $"Pregunta {preguntaActual + 1}/{preguntasAleatorias.Count}";

        for (int i = 0; i < botonesOpciones.Length; i++)
        {
            bool opcionValida = i < pregunta.opciones.Length;

            botonesOpciones[i].gameObject.SetActive(opcionValida);
            if (opcionValida)
            {
                botonesOpciones[i].GetComponentInChildren<TextMeshProUGUI>().text = pregunta.opciones[i];
                botonesOpciones[i].image.color = colorNormal;
                botonesOpciones[i].interactable = true;
            }
        }

        esperandoRespuesta = true;
    }

    public void Responder(int indiceOpcion)
    {
        if (!esperandoRespuesta) return;

        esperandoRespuesta = false;
        PreguntaSO pregunta = preguntasAleatorias[preguntaActual];

        foreach (var boton in botonesOpciones)
        {
            if (boton.gameObject.activeSelf)
            {
                boton.interactable = false;
            }
        }

        bool esCorrecta = indiceOpcion == pregunta.respuestaCorrecta;
        if (esCorrecta) aciertos++;

        // Reproducir sonido según la respuesta
        if (audioSource != null)
        {
            audioSource.clip = esCorrecta ? sonidoCorrecto : sonidoIncorrecto;
            audioSource.Play();
        }

        botonesOpciones[indiceOpcion].image.color = esCorrecta ? colorCorrecto : colorIncorrecto;
        if (!esCorrecta && pregunta.respuestaCorrecta < botonesOpciones.Length)
        {
            botonesOpciones[pregunta.respuestaCorrecta].image.color = colorCorrecto;
        }

        StartCoroutine(SiguientePregunta());
    }

    IEnumerator SiguientePregunta()
    {
        yield return new WaitForSeconds(tiempoFeedback);
        preguntaActual++;
        MostrarPregunta();
    }

    void FinalizarTest()
    {
        PlayerPrefs.SetInt("Aciertos", aciertos);
        PlayerPrefs.SetInt("TotalPreguntas", preguntasAleatorias.Count);
        PlayerPrefs.Save();

        SceneManager.LoadScene(escenaResultados);
    }

    public void SalirAlMenu()
    {
        SceneManager.LoadScene("SMenu");
    }
}
