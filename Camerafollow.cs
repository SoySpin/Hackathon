using UnityEngine;

public class Camerollow : MonoBehaviour
{
    public Transform car;        // Referencia al coche
    public float followSpeed = 5f; // Velocidad de seguimiento de la c�mara
    public Vector3 offset = new Vector3(0, 10, -10); // Desplazamiento de la c�mara (aj�stalo seg�n lo que necesites)

    private void Update()
    {
        if (car != null)
        {
            // Calcula la nueva posici�n de la c�mara en funci�n de la posici�n del coche y el offset
            Vector3 targetPosition = car.position + offset;
            // Mueve la c�mara hacia la nueva posici�n de manera suave
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}