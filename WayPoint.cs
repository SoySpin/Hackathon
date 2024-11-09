using UnityEngine;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour
{
    public Vector3 position;  // Cambiar a Vector3
    public List<Waypoint> neighbors = new List<Waypoint>();  // Lista de vecinos conectados

    private void Awake()
    {
        position = transform.position;  // Usa transform.position, que devuelve un Vector3
    }
}