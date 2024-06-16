using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 followOffset;

    private void Start()
    {
        // Asegurarse de que se asignó la cámara virtual
        if (virtualCamera == null)
        {
            Debug.LogError("No se asignó una Virtual Camera de Cinemachine en el Inspector.");
            return;
        }

        // Asegurarse de que se asignó el objetivo de seguimiento
        if (target == null)
        {
            Debug.LogError("No se asignó un objetivo de seguimiento en el Inspector.");
            return;
        }

        // Configurar el objetivo de seguimiento de la cámara
        virtualCamera.Follow = target;

        // Configurar el offset de seguimiento
        virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = followOffset;
    }

    // Método para actualizar el objetivo de seguimiento dinámicamente
    public void SetTarget(Transform newTarget)
    {
        if (newTarget != null)
        {
            target = newTarget;
            virtualCamera.Follow = target;
        }
    }

    // Método para actualizar el offset de seguimiento dinámicamente
    public void SetFollowOffset(Vector3 newOffset)
    {
        followOffset = newOffset;
        virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = followOffset;
    }
}
