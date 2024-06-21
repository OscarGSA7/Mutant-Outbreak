using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform jugador; 
    public float velocidadMovimiento = 2.0f; 
    public int daño = 20; 
    
    [Header("Animacion")]
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (jugador != null)
        {
            // Calcular la dirección y moverse hacia el jugador
            Vector3 direccion = (jugador.position - transform.position).normalized;
            transform.position += direccion * velocidadMovimiento * Time.deltaTime;

            // Actualizar la animación del movimiento
            if (direccion != Vector3.zero)
            {
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            Jugador jugadorScript = other.GetComponent<Jugador>();
            if (jugadorScript != null)
            {
                jugadorScript.TomarDaño(daño);
            }
        }
    }
}
