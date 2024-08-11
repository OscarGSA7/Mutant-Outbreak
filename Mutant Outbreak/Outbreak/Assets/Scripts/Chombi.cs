using UnityEngine;

public class Zombie : MonoBehaviour
{
    private ControladorJuego controlador;
    private ControladorEstadisticas controladorEstadisticas;

    private void Start()
    {
        controlador = FindObjectOfType<ControladorJuego>();
        controladorEstadisticas = FindObjectOfType<ControladorEstadisticas>();
    }

    private void OnDestroy()
    {

        if (controladorEstadisticas != null)
        {
            controladorEstadisticas.RegistrarZombieEliminado();
        }
    }
}
