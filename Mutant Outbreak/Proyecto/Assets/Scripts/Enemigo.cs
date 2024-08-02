using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float velocidadMovimiento = 2.0f; 
    public int daño = 20; 
    public int vidaMaxima = 100;
    private int vidaActual;
    public GameObject prefabBarraDeVidaZombi;
    public BarraDeVidaZombie barraDeVida { get; set; }
    public bool isDead = false;
    private Transform jugador; 
    private Animator animator; 
    private Vector2 direccion; 
    private bool puedeHacerDaño = true; 

    private void Start()
    {
        animator = GetComponent<Animator>(); 
        jugador = GameObject.FindWithTag("Jugador").transform; 
        vidaActual = vidaMaxima;
        
        
        GameObject barra = Instantiate(prefabBarraDeVidaZombi, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform);
        barraDeVida = barra.GetComponent<BarraDeVidaZombie>();

        
        barraDeVida.ActualizarVida(vidaActual, vidaMaxima);
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
        vidaActual -= daño;

        barraDeVida.ActualizarVida(vidaActual, vidaMaxima);

        
        
        if (daño > 0 && !isDead)
    {
        animator.SetBool("Daño", true);
        Invoke("DesactivarDaño", 0.3f);
    }

        if (vidaActual <= 0)
        {
            Muerte();
        }
    }
    private void DesactivarDaño()
    {
        animator.SetBool("Daño", false);
    }

    private void Muerte()
    {

        velocidadMovimiento = 0;
        isDead = true;
        animator.SetTrigger("isDead");
        Destroy(gameObject, 0.6f);
    }
}
