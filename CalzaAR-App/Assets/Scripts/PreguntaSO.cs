using UnityEngine;

[CreateAssetMenu(fileName = "NuevaPregunta", menuName = "Senal/Pregunta")]
public class PreguntaSO : ScriptableObject
{
    public Sprite imagenSe�al;        // Imagen de la se�al relacionada
    public string pregunta;           // Texto de la pregunta
    public string[] opciones;        // Opciones de respuesta (m�nimo 3)
    public int respuestaCorrecta;    // �ndice de la opci�n correcta (ej: 0 = opci�n A)
}