using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este menú permite crear nuevos ScriptableObjects desde el Editor de Unity
[CreateAssetMenu(fileName = "NuevaSenal", menuName = "Senaletica/Senal")]
public class SenalSO : ScriptableObject
{
    public enum TipoSenal { Preventiva, Restrictiva, Informativas }

    [Header("Tipo de Senal")]
    public TipoSenal tipo;

    [Header("Contenido Visual")]
    public Sprite imagen;   // Arrastra aquí el Sprite de la señal
    public string nombre;   // Ej: "Pare", "Ceda el Paso"

    [Header("Descripción")]
    [TextArea(3, 5)]        // Caja de texto multilínea en el Inspector
    public string descripcion;
}