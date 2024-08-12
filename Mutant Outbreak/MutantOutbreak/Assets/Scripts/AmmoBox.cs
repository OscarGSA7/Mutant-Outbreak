using UnityEngine;
using UnityEngine.UI;

public class AmmoRefill : MonoBehaviour
{
    public int costToRefill = 1000;
    public Text interactionText;
    public GameObject player; 
    public ControladorDinero controladorDinero; 
    public ControlArma controlArma; 
    private bool playerInRange = false;
    private Controles controles;

    private void Awake()
    {
        controles = new Controles();
        controles.Base.Interactuar.performed += ctx => RefillAmmo();
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
        interactionText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange)
        {
            interactionText.gameObject.SetActive(true);
            interactionText.text = "Presiona 'F' para munición (costo: " + costToRefill + " puntos)";
        }
        else
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            
            playerInRange = false;
        }
    }

    private void RefillAmmo()
    {
        if (!playerInRange) return;

        if (controlArma != null && controladorDinero != null)
        {
            
            if (controladorDinero.cantidad >= costToRefill)
            {
                
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
            
        }
    }
}
