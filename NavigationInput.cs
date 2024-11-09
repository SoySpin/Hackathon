using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NavigationInput : MonoBehaviour
{
    public TMP_Dropdown startDropdown;              // Dropdown para seleccionar el punto de salida
    public TMP_Dropdown destinationDropdown;        // Dropdown para seleccionar el destino
    public NavigationSystem navigationSystem;       // Referencia al sistema de navegación
    public Waypoint[] waypoints;                    // Todos los waypoints disponibles

    private void Start()
    {
        // Llenamos los dropdowns con los nombres de los waypoints
        PopulateDropdown(startDropdown);
        PopulateDropdown(destinationDropdown);

        // Asignar listeners para cambios en el dropdown
        startDropdown.onValueChanged.AddListener(OnStartSelected);
        destinationDropdown.onValueChanged.AddListener(OnDestinationSelected);
    }

    // Llenamos el Dropdown con los tags de los waypoints
    private void PopulateDropdown(TMP_Dropdown dropdown)
    {
        dropdown.ClearOptions();
        foreach (var waypoint in waypoints)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(waypoint.gameObject.tag));  // Usamos el tag del waypoint como opción
        }
        dropdown.RefreshShownValue();
    }

    // Se llama cuando el jugador selecciona un punto de salida
    private void OnStartSelected(int index)
    {
        string startTag = startDropdown.options[index].text;
        Waypoint startWaypoint = FindWaypointByTag(startTag);
        if (startWaypoint != null)
        {
            navigationSystem.SetStartWaypoint(startWaypoint);  // Establece el punto de salida
            Debug.Log("Punto de salida actualizado: " + startTag);

            // Mueve el coche a la posición del punto de salida
            navigationSystem.MoveCarToStart();
        }
        else
        {
            Debug.LogWarning("Punto de salida no encontrado: " + startTag);
        }
    }

    // Se llama cuando el jugador selecciona un destino
    private void OnDestinationSelected(int index)
    {
        string destinationTag = destinationDropdown.options[index].text;
        Waypoint destinationWaypoint = FindWaypointByTag(destinationTag);
        if (destinationWaypoint != null)
        {
            navigationSystem.SetDestination(destinationWaypoint);  // Establece el destino
            Debug.Log("Destino actualizado: " + destinationTag);

            // Inicia la navegación hacia el destino
            navigationSystem.TriggerNavigation();
        }
        else
        {
            Debug.LogWarning("Destino no encontrado: " + destinationTag);
        }
    }

    // Buscar el waypoint por su tag
    private Waypoint FindWaypointByTag(string tag)
    {
        foreach (var waypoint in waypoints)
        {
            if (waypoint.gameObject.CompareTag(tag))  // Compara el tag del waypoint con el input
            {
                return waypoint;
            }
        }
        return null;
    }
}