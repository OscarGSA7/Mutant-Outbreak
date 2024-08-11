using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip Puerta;
    public int costToOpen = 3000;
    public Text interactionText;
    public GameObject player;  
    public float interactionDistance = 2.0f;
    private bool isOpen = false;

    private Collider2D doorCollider;
    private Animator animator;
    private Controles controles;

    private void Awake()
    {
        controles = new Controles();
        controles.Base.Interactuar.performed += ctx => TryOpenDoor();
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
        doorCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isOpen) return; // Si la puerta ya está abierta, no hacer nada

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= interactionDistance)
        {
            interactionText.gameObject.SetActive(true);
        }
        else
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    private void TryOpenDoor()
    {
        if (isOpen) return; // Si la puerta ya está abierta, no hacer nada

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= interactionDistance)
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
