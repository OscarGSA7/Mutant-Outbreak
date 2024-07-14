using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    public int vida = 100;
    public int vidaMaxima = 100;
    public int cantidadRegeneracion = 10;
    public float tiempoRegeneracionBase = 3.0f; // Tiempo de regeneración base
    public float ajusteTiempoRegeneracion = 1.0f; // Ajuste del tiempo de regeneración (multiplicador)
    public float tiempoRegeneracion { get { return tiempoRegeneracionBase * ajusteTiempoRegeneracion; } } // Tiempo de regeneración actual

    public BarraDeVida barraDeVida;

    public Transform puntoDisparo; 
    public GameObject prefabBala; 

    private Vector2 direccionDisparo = Vector2.right;

    private void Start()
    {
        StartCoroutine(RegenerarVida());
        barraDeVida.InicializarBarraDeVida(vida);
    }

    private void Update()
    {
        // Determinar la dirección de disparo
        if (Input.GetKey(KeyCode.W)) direccionDisparo = Vector2.up;
        if (Input.GetKey(KeyCode.A)) direccionDisparo = Vector2.left;
        if (Input.GetKey(KeyCode.S)) direccionDisparo = Vector2.down;
        if (Input.GetKey(KeyCode.D)) direccionDisparo = Vector2.right;

        
        if (Input.GetMouseButtonDown(0))
        {
            Disparar();
        }
    }

    public void TomarDaño(int daño)
    {
        vida -= daño;
        Debug.Log("Jugador ha tomado daño. Vida restante: " + vida);
        barraDeVida.CambiarVidaActual(vida);

        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        Debug.Log("Jugador ha muerto.");
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    private IEnumerator RegenerarVida()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoRegeneracion);

            if (vida > 0 && vida < vidaMaxima)
            {
                vida += cantidadRegeneracion;

                if (vida > vidaMaxima)
                {
                    vida = vidaMaxima;
                }

                Debug.Log("Jugador ha regenerado vida. Vida actual: " + vida);
                barraDeVida.CambiarVidaActual(vida);
            }
        }
    }

    private void Disparar()
    {
        GameObject bala = Instantiate(prefabBala, puntoDisparo.position, Quaternion.identity);
        Bala balaScript = bala.GetComponent<Bala>();
        if (balaScript!= null)
        {
            balaScript.DireccionDisparo(direccionDisparo);
        }
    }
}