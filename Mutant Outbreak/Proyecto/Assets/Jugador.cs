using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    public int vida = 100; 
    public int vidaMaxima = 100; 
    public int cantidadRegeneracion = 10; 
    public float tiempoRegeneracion = 3.0f; 
    public BarraDeVida barraDeVida;

    private void Start()
    {
        
        StartCoroutine(RegenerarVida());
        
        barraDeVida.InicializarBarraDeVida(vida);
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
}
