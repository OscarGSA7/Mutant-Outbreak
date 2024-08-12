using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip SondioGolpe;
    public float velocidadMovimiento = 2.0f; 
    public float daño = 20; 
    public float vidaMaxima = 100;
    private float vidaActual;
    public GameObject prefabBarraDeVidaZombi;
    public BarraDeVidaZombie barraDeVida { get; set; }
    public bool isDead = false;
    private Transform jugador; 
    private Animator animator; 
    private Vector2 direccion; 
    private bool puedeHacerDaño = true;
    private ControladorDinero controladorDinero;
    private Bala bala;

    private void Start()
    {
        animator = GetComponent<Animator>(); 
        controladorDinero = FindObjectOfType<ControladorDinero>();
        jugador = GameObject.FindWithTag("Jugador").transform; 
        vidaActual = vidaMaxima;
        
        // Instanciar y asignar la barra de vida
        GameObject barra = Instantiate(prefabBarraDeVidaZombi, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform);
        barraDeVida = barra.GetComponent<BarraDeVidaZombie>();
        barraDeVida.ActualizarVida(vidaActual, vidaMaxima);
    }

    private void Update()
    {
        if (jugador != null && !isDead)
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
                    jugadorScript.TomarDaño((int)daño);
                    StartCoroutine(CooldownDaño());
                }
            }
        }
        else if (other.CompareTag("Bala"))
        {
            Bala balaScript = other.GetComponent<Bala>();
            if (balaScript != null)
            {
                RecibirDaño((int)balaScript.daño);
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
        if (audioSource != null && SondioGolpe != null)
        {
            audioSource.PlayOneShot(SondioGolpe);
        }
        vidaActual -= daño;
        Debug.Log("Recibiendo daño: " + daño);
        Debug.Log("Vida es:"+ vidaActual);
        barraDeVida.ActualizarVida(vidaActual, vidaMaxima);

        if (daño > 0 && !isDead)
        {
            animator.SetBool("Daño", true);
            Invoke("DesactivarDaño", 0.3f);
        }

        if (vidaActual <= 0 && !isDead)
        {
            Muerte();
        }
    }

    private void DesactivarDaño()
    {
        animator.SetBool("Daño", false);
    }

    public void AjustarVidaPorRonda(int rondaActual)
    {
        vidaMaxima = 100 + (10 * rondaActual);
        vidaActual = vidaMaxima;
        
        
        daño = 20 + (5 * rondaActual);


        if (barraDeVida != null)
        {
            barraDeVida.ActualizarVida(vidaActual, vidaMaxima);
        }
        
    }

    private void Muerte()
    {
        velocidadMovimiento = 0;
        isDead = true;
        animator.SetTrigger("isDead");
        controladorDinero.AgregarDinero(100);
        Destroy(gameObject, 0.6f);
    }
}
