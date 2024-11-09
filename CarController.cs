using UnityEngine;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
    public float speed = 5f;  // Velocidad del coche
    public float obstacleCheckDistance = 2f;  // Distancia a la que el coche revisa si hay un obst�culo
    private List<Vector3> route;  // Ruta a seguir
    private int currentWaypointIndex = 0;  // �ndice del waypoint actual

    // M�todo para verificar si hay un obst�culo en la direcci�n del coche
    private bool IsObstacleAhead(Vector3 direction)
    {
        RaycastHit hit;
        // Lanza un raycast hacia la direcci�n del coche
        if (Physics.Raycast(transform.position, direction, out hit, obstacleCheckDistance))
        {
            // Si el objeto que toc� el raycast tiene el tag "Obst�culo"
            if (hit.collider.CompareTag("Obst�culo"))
            {
                return true; // Obst�culo detectado
            }
        }
        return false; // No hay obst�culo
    }

    // M�todo para mover el coche
    private void MoveCar(Vector3 direction)
    {
        if (!IsObstacleAhead(direction))  // Solo mueve el coche si no hay obst�culos
        {
            // Mueve el coche, pero asegur�ndose de que la coordenada Y no cambie
            Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
            newPosition.y = transform.position.y;  // Mant�n la coordenada Y constante
            transform.position = newPosition;
        }
        else
        {
            Debug.Log("Obst�culo detectado, no se puede mover.");
        }
    }

    // M�todo para evitar obst�culos movi�ndote en una direcci�n alternativa
    private void AvoidObstacle()
    {
        // Determinar una direcci�n alternativa (por ejemplo, a la derecha)
        Vector3 alternativeDirection = transform.right;  // O podr�as usar transform.left, dependiendo de tu l�gica

        // Verificar si el coche puede moverse en esa direcci�n
        if (!IsObstacleAhead(alternativeDirection))
        {
            // Mueve el coche, pero asegur�ndose de que la coordenada Y no cambie
            Vector3 newPosition = transform.position + alternativeDirection * speed * Time.deltaTime;
            newPosition.y = transform.position.y;  // Mant�n la coordenada Y constante
            transform.position = newPosition;
            Debug.Log("Evadiendo obst�culo.");
        }
        else
        {
            Debug.Log("No hay espacio para evadir el obst�culo.");
        }
    }

    // M�todo para establecer la ruta
    public void SetRoute(List<Vector3> newRoute)
    {
        route = newRoute;
        currentWaypointIndex = 0;  // Comienza desde el primer waypoint
    }

    // M�todo para actualizar la posici�n del coche en cada frame
    private void Update()
    {
        if (route != null && route.Count > 0)
        {
            // Calcula la direcci�n hacia el siguiente waypoint
            Vector3 targetPosition = route[currentWaypointIndex];

            // Calculamos la diferencia en X y Z
            float deltaX = targetPosition.x - transform.position.x;
            float deltaZ = targetPosition.z - transform.position.z;

            Vector3 direction = Vector3.zero;

            // Movemos solo en una direcci�n, ya sea X o Z
            if (Mathf.Abs(deltaX) > Mathf.Abs(deltaZ))
            {
                // Si la diferencia en X es mayor, movemos en el eje X
                direction = new Vector3(Mathf.Sign(deltaX), 0, 0);
            }
            else
            {
                // Si la diferencia en Z es mayor, movemos en el eje Z
                direction = new Vector3(0, 0, Mathf.Sign(deltaZ));
            }

            // Si el coche est� cerca del waypoint, avanza al siguiente
            if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= route.Count)
                {
                    Debug.Log("El coche ha llegado al destino.");
                }
            }
            else
            {
                // Mueve el coche solo en una direcci�n (X o Z)
                MoveCar(direction);
            }
        }
        else
        {
            Debug.LogWarning("No se ha establecido una ruta.");
        }
    }
}