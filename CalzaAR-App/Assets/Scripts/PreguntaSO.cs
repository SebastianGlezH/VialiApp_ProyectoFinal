using UnityEngine;

[CreateAssetMenu(fileName = "NuevaPregunta", menuName = "Senal/Pregunta")]
public class PreguntaSO : ScriptableObject
{
    public Sprite imagenSeñal;        // Imagen de la señal relacionada
    public string pregunta;           // Texto de la pregunta
    public string[] opciones;        // Opciones de respuesta (mínimo 3)
    public int respuestaCorrecta;    // Índice de la opción correcta (ej: 0 = opción A)
}