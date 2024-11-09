using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public List<Waypoint> waypoints;  // Lista de waypoints disponibles

    // Encuentra una ruta desde el waypoint de inicio al de destino
    public List<Waypoint> FindPath(Waypoint start, Waypoint destination)
    {
        // Aquí puedes agregar el algoritmo de pathfinding (como BFS, A*, etc.), por ahora simularemos una lista
        List<Waypoint> path = new List<Waypoint>();
        path.Add(start);  // El punto de inicio
        path.Add(destination);  // El punto de destino
        return path;
    }

    // Método para dibujar la ruta con Gizmos
    private void OnDrawGizmos()
    {
        if (waypoints != null && waypoints.Count > 0)
        {
            Gizmos.color = Color.green;  // Color de la ruta
            for (int i = 0; i < waypoints.Count - 1; i++)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);  // Dibuja línea entre waypoints
            }

            // Dibujar esferas en los waypoints
            foreach (var waypoint in waypoints)
            {
                Gizmos.DrawSphere(waypoint.position, 0.2f);  // Tamaño de la esfera
            }
        }
    }
}