using UnityEngine;

public class Camerollow : MonoBehaviour
{
    public Transform car;        // Referencia al coche
    public float followSpeed = 5f; // Velocidad de seguimiento de la cámara
    public Vector3 offset = new Vector3(0, 10, -10); // Desplazamiento de la cámara (ajústalo según lo que necesites)

    private void Update()
    {
        if (car != null)
        {
            // Calcula la nueva posición de la cámara en función de la posición del coche y el offset
            Vector3 targetPosition = car.position + offset;
            // Mueve la cámara hacia la nueva posición de manera suave
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}