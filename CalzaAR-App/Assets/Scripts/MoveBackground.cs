using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    [SerializeField] private MeshRenderer mesh;

    public void Update()
    {
        Vector2 offset = new Vector2( -Time.time * scrollSpeed,0);
        mesh.material.mainTextureOffset = offset;
    }
}