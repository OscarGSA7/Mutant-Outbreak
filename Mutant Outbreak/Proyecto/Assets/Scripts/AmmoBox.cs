using UnityEngine;
using UnityEngine.UI;

public class AmmoRefill : MonoBehaviour
{
    public int costToRefill = 1000;
    public Text interactionText;
    public GameObject player; // Asigna el jugador desde el editor
    public ControladorDinero controladorDinero; // Asigna el ControladorDinero desde el editor
    public ControlArma controlArma; // Asigna el ControlArma desde el editor
    private bool playerInRange = false;

    private void Start()
    {
        interactionText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("F presionado");
                if (controlArma != null && controladorDinero != null)
                {
                    Debug.Log("ControlArma y ControladorDinero no son nulos");
                    if (controladorDinero.cantidad >= costToRefill)
                    {
                        Debug.Log("Suficiente dinero, reponiendo munición");
                        controladorDinero.QuitarDinero(costToRefill);
                        controlArma.RefillAmmo();
                        interactionText.gameObject.SetActive(false);
                    }
                    else
                    {
                        Debug.Log("No tienes suficientes puntos.");
                    }
                }
                else
                {
                    Debug.Log("ControlArma o ControladorDinero son nulos");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Jugador entró en el rango de interacción");
            playerInRange = true;
            interactionText.gameObject.SetActive(true);
            interactionText.text = "Presiona 'F' para munición (costo: " + costToRefill + " puntos)";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Jugador salió del rango de interacción");
            playerInRange = false;
            interactionText.gameObject.SetActive(false);
        }
    }
}
