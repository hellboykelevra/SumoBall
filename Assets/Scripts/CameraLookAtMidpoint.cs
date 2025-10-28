using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAtMidpoint : MonoBehaviour
{
    // Atributos públicos para asignar los dos GameObjects en el Inspector
    public Transform object1;
    public Transform object2;

    // Distancia máxima y mínima a la que puede estar la cámara
    public float minDistance = 5f;
    public float maxDistance = 20f;

    // Factor para ajustar qué tan rápido se aleja/acerca la cámara
    public float distanceFactor = 1.2f;

    // Velocidad de suavizado para el movimiento
    public float smoothTime = 0.5f;

    // Variables privadas para el cálculo
    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 initialOffset;

    void Start()
    {
        // Calcular el desplazamiento inicial de la cámara con respecto al punto medio
        if (object1 != null && object2 != null)
        {
            Vector3 midpoint = (object1.position + object2.position) / 2f;
            initialOffset = transform.position - midpoint;
        }
    }

    void LateUpdate()
    {
        // Verificar que los dos objetos están asignados para evitar errores
        if (object1 == null || object2 == null)
        {
            Debug.LogWarning("Uno o ambos GameObjects de destino no están asignados en el script CameraLookAtMidpoint.");
            return;
        }

        // 1. Calcular el punto medio entre las posiciones de los dos objetos
        Vector3 midpoint = (object1.position + object2.position) / 2f;

        // 2. Calcular la distancia entre los dos objetos
        float distance = Vector3.Distance(object1.position, object2.position);

        // 3. Calcular la distancia objetivo de la cámara basada en la distancia entre los objetos
        // Se usa Mathf.Clamp para asegurar que la distancia se mantenga entre los límites definidos
        float targetDistance = Mathf.Clamp(distance * distanceFactor, minDistance, maxDistance);

        // 4. Calcular la nueva posición de la cámara
        Vector3 targetPosition = midpoint + initialOffset.normalized * targetDistance;

        // 5. Mover la cámara de forma suave a la nueva posición
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // 6. La cámara rota para mirar al punto medio
        transform.LookAt(midpoint);
    }
}
