using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip Puerta;
    public int costToOpen = 3000;
    public Text interactionText;
    public GameObject player;  // Asigna el jugador desde el editor
    public float interactionDistance = 2.0f;
    private bool isOpen = false;

    private Collider2D doorCollider;
    private Animator animator;

    private void Start()
    {
        interactionText.gameObject.SetActive(false);
        doorCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= interactionDistance && !isOpen)
        {
            interactionText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                ControladorDinero controladorDinero = player.GetComponent<ControladorDinero>();
                if (controladorDinero != null)
                {
                    if (controladorDinero.QuitarDinero(costToOpen))
                    {
                        OpenDoor();
                    }
                    else
                    {
                        Debug.Log("No tienes suficientes puntos para abrir la puerta.");
                    }
                }
            }
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
            interactionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    private void OpenDoor()
    {
        if (audioSource != null && Puerta != null)
        {
            audioSource.PlayOneShot(Puerta);
        }
        isOpen = true;
        interactionText.gameObject.SetActive(false);
        doorCollider.enabled = false;
        animator.SetTrigger("Open"); // Iniciar la animación
        Debug.Log("La puerta se ha abierto.");
    }
}
