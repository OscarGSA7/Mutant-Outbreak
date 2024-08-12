using UnityEngine;
using UnityEngine.UI;

public class BoostVelocidad : MonoBehaviour
{
    public int costo = 3000; 
    public Text textoInteraccion;
    public GameObject jugador; 
    public ControladorDinero controladorDinero; 
    public Movimiento movimientoJugador; 
    private Controles controles;

    private bool jugadorEnRango = false;
    public bool yaUsado = false;

    private void Awake()
    {
        controles = new Controles();
        controles.Base.Interactuar.performed += ctx => AplicarBoostVelocidad();
    }

    private void OnEnable()
    {
        controles.Enable();
    }

    private void OnDisable()
    {
        controles.Disable();
    }

    private void Start()
    {
        textoInteraccion.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (jugadorEnRango && !yaUsado)
        {
            textoInteraccion.gameObject.SetActive(true);
        }
        else
        {
            textoInteraccion.gameObject.SetActive(false);
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

    private void AplicarBoostVelocidad()
    {
        if (!jugadorEnRango || yaUsado) return;

        if (controladorDinero != null && movimientoJugador != null)
        {
            if (controladorDinero.cantidad >= costo)
            {
                controladorDinero.QuitarDinero(costo);
                movimientoJugador.IncrementarVelocidad(3.0f); 
                yaUsado = true;
                textoInteraccion.gameObject.SetActive(false);
                Debug.Log("Velocidad aumentada en 3 puntos.");
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
