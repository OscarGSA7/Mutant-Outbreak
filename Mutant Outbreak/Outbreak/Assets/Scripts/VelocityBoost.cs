using UnityEngine;
using UnityEngine.UI;

public class BoostVelocidad : MonoBehaviour
{
    public int costo = 3000; // Costo para usar el boost
    public Text textoInteraccion;
    public GameObject jugador; // Asigna el jugador desde el editor
    public ControladorDinero controladorDinero; // Asigna el ControladorDinero desde el editor
    public Movimiento movimientoJugador; // Asigna el componente Movimiento desde el editor

    private bool jugadorEnRango = false;
    public bool yaUsado = false;

    private void Start()
    {
        textoInteraccion.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (jugadorEnRango && !yaUsado)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (controladorDinero != null && movimientoJugador != null)
                {
                    if (controladorDinero.cantidad >= costo)
                    {
                        controladorDinero.QuitarDinero(costo);
                        movimientoJugador.IncrementarVelocidad(3.0f); // Incrementa la velocidad en 3 puntos
                        yaUsado = true;
                        textoInteraccion.gameObject.SetActive(false);
                    }
                    else
                    {
                        Debug.Log("No tienes suficientes puntos para el boost.");
                    }
                }
                else
                {
                    Debug.Log("No se encontraron todos los componentes necesarios.");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == jugador && !yaUsado)
        {
            jugadorEnRango = true;
            textoInteraccion.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == jugador)
        {
            jugadorEnRango = false;
            textoInteraccion.gameObject.SetActive(false);
        }
    }

}
