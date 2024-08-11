using UnityEngine;

public class TopDownCameraFollow : MonoBehaviour
{
    public Transform jugador; // Referencia al transform del jugador
    public Vector3 offset; // Desplazamiento de la cámara respecto al jugador

    private void Start()
    {
        // Verificar si el jugador fue asignado
        if (jugador == null)
        {
            Debug.LogError("No se asignó el jugador al script de seguimiento de cámara.");
        }
    }

    private void LateUpdate()
    {
        if (jugador != null)
        {
            // Actualizar la posición de la cámara para seguir al jugador con el offset
            transform.position = new Vector3(jugador.position.x, jugador.position.y, transform.position.z) + offset;
        }
    }
}
