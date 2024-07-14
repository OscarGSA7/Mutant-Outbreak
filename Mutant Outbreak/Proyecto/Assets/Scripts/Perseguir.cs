using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float velocidadMovimiento = 2.0f; 
    public int daño = 20; 
    public int vida = 100; 

    private Transform jugador; 
    private Animator animator; 
    private Vector2 direccion; 
    private bool puedeHacerDaño = true; 

    private void Start()
    {
        animator = GetComponent<Animator>(); 
        jugador = GameObject.FindWithTag("Jugador").transform; 
    }

    private void Update()
    {
        if (jugador != null)
        {
            
            direccion = (jugador.position - transform.position).normalized;

            
            if (direccion != Vector2.zero)
            {
                animator.SetFloat("Horizontal", direccion.x);
                animator.SetFloat("Vertical", direccion.y);
            }

            
            transform.position += (Vector3)direccion * velocidadMovimiento * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            if (puedeHacerDaño)
            {
                Jugador jugadorScript = other.GetComponent<Jugador>();
                if (jugadorScript != null)
                {
                    jugadorScript.TomarDaño(daño);
                    StartCoroutine(CooldownDaño());
                }
            }
        }
        else if (other.CompareTag("Bala"))
        {
            Bala balaScript = other.GetComponent<Bala>();
            if (balaScript != null)
            {
                RecibirDaño(balaScript.daño);
                Destroy(other.gameObject); 
            }
        }
    }

    private IEnumerator CooldownDaño()
    {
        puedeHacerDaño = false;
        yield return new WaitForSeconds(1.0f); 
        puedeHacerDaño = true;
    }

    public void RecibirDaño(int daño)
    {
        vida -= daño;
        Debug.Log("Daño hacia chombi de: " + daño);
        Debug.Log("Vida de zombie:" + vida);
        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        Destroy(gameObject);
    }
}
