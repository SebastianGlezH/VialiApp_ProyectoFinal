using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este menú permite crear nuevos ScriptableObjects desde el Editor de Unity
[CreateAssetMenu(fileName = "NuevaSeñal", menuName = "Señalética/Señal")]
public class SeñalSO : ScriptableObject
{
    public enum TipoSeñal { Preventiva, Restrictiva }

    [Header("Tipo de Señal")]
    public TipoSeñal tipo;

    [Header("Contenido Visual")]
    public Sprite imagen;  // Arrastra aquí el Sprite de la señal
    public string nombre;  // Ej: "Pare", "Ceda el Paso"

    [Header("Descripción")]
    [TextArea(3, 5)]       // Caja de texto multilínea en el Inspector
    public string descripcion;
}