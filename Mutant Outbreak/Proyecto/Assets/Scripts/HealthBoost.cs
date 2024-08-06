using UnityEngine;
using UnityEngine.UI;

public class HealthBoost : MonoBehaviour
{
    public int costToBoost = 5000;
    public Text interactionText;
    public GameObject player; // Asigna el jugador desde el editor
    public ControladorDinero controladorDinero; // Asigna el ControladorDinero desde el editor

    private bool playerInRange = false;
    private bool hasBeenUsed = false;
    private Jugador jugador;

    private void Start()
    {
        interactionText.gameObject.SetActive(false);
        jugador = player.GetComponent<Jugador>();
    }

    public bool HasBeenUsed()
    {
        return hasBeenUsed;
    }

    private void Update()
    {
        if (playerInRange && !hasBeenUsed)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (controladorDinero != null && jugador != null)
                {
                    if (controladorDinero.cantidad >= costToBoost)
                    {
                        controladorDinero.QuitarDinero(costToBoost);
                        jugador.vidaMaxima = 200;
                        jugador.vida = 200;
                        jugador.barraDeVida.CambiarVidaMaxima(jugador.vidaMaxima);
                        jugador.barraDeVida.CambiarVidaActual(jugador.vida);
                        hasBeenUsed = true;
                        interactionText.gameObject.SetActive(false);
                        Debug.Log("Has aumentado tu vida máxima a 200 puntos.");
                    }
                    else
                    {
                        Debug.Log("No tienes suficientes puntos para aumentar tu vida.");
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
        if (other.gameObject == player && !hasBeenUsed)
        {
            playerInRange = true;
            interactionText.gameObject.SetActive(true);
            Debug.Log("Jugador en rango.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            interactionText.gameObject.SetActive(false);
            Debug.Log("Jugador fuera de rango.");
        }
    }
}
