using UnityEngine;
using System.Collections.Generic;

public class NavigationSystem : MonoBehaviour
{
    public CarController carController;  // Asegúrate de que esta referencia esté asignada en el Inspector
    public Pathfinding pathfinding;      // Asegúrate de que esta referencia también esté asignada

    private Waypoint startWaypoint;
    private Waypoint destinationWaypoint;

    // Establece el waypoint de inicio
    public void SetStartWaypoint(Waypoint newStart)
    {
        startWaypoint = newStart;
        TryStartNavigation();
    }

    // Establece el waypoint de destino
    public void SetDestination(Waypoint newDestination)
    {
        destinationWaypoint = newDestination;
        TryStartNavigation();
    }

    // Intenta iniciar la navegación si ambos waypoints están configurados
    private void TryStartNavigation()
    {
        if (startWaypoint != null && destinationWaypoint != null)
        {
            StartNavigation();
        }
    }

    // Inicia la navegación entre el punto de salida y el destino
    private void StartNavigation()
    {
        // Usa el sistema de pathfinding para encontrar la ruta
        List<Waypoint> route = pathfinding.FindPath(startWaypoint, destinationWaypoint);

        if (route.Count == 0)
        {
            Debug.Log("No se encontró ruta al destino.");
        }
        else
        {
            Debug.Log("Ruta generada hacia el destino.");
            List<Vector3> routeVector3 = new List<Vector3>();

            // Convierte la lista de waypoints en una lista de posiciones (Vector3)
            foreach (var waypoint in route)
            {
                routeVector3.Add(waypoint.transform.position);
            }

            // Establece la ruta en el controlador del coche
            carController.SetRoute(routeVector3);  // Llama al método SetRoute en CarController
        }
    }

    // Mueve el coche a la posición del punto de salida
    public void MoveCarToStart()
    {
        if (startWaypoint != null)
        {
            carController.transform.position = startWaypoint.transform.position;  // Mueve el coche al punto de inicio
        }
    }

    // Inicia la navegación al destino una vez seleccionado el punto de inicio
    public void TriggerNavigation()
    {
        if (startWaypoint != null && destinationWaypoint != null)
        {
            StartNavigation();
        }
    }
}