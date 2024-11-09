using UnityEngine;
using System.Collections.Generic;

public class PathVisualizer : MonoBehaviour
{
    public Pathfinding pathfinding;  // Referencia al sistema de pathfinding
    public Waypoint startWaypoint;   // El punto de salida
    public Waypoint destinationWaypoint; // El punto de destino
    private List<Waypoint> route;    // La ruta calculada

    void OnDrawGizmos()
    {
        if (pathfinding != null && startWaypoint != null && destinationWaypoint != null)
        {
            route = pathfinding.FindPath(startWaypoint, destinationWaypoint);

            if (route != null && route.Count > 0)
            {
                // Dibuja la línea de la ruta
                Gizmos.color = Color.green;  // Color de la ruta
                for (int i = 0; i < route.Count - 1; i++)
                {
                    Gizmos.DrawLine(route[i].position, route[i + 1].position);
                }
            }
        }
    }
}