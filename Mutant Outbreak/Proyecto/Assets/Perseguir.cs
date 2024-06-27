using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform jugador; 
    public float velocidadMovimiento = 2.0f; 
    public int daño = 20; 
    
    private Animator animator;
    private Vector2 direccionMovimiento;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (jugador != null)
        {
            
            Vector3 direccion = (jugador.position - transform.position).normalized;
            direccionMovimiento = new Vector2(direccion.x, direccion.y);

            
            transform.position += (Vector3)direccionMovimiento * velocidadMovimiento * Time.deltaTime;

            
            animator.SetFloat("MovimientoX", direccionMovimiento.x);
            
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
