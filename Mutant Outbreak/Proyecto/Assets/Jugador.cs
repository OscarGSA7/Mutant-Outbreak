using UnityEngine;
using System.Collections;

public class Jugador : MonoBehaviour
{
    public int vida = 100; 
    public int vidaMaxima = 100; 
    public int cantidadRegeneracion = 10; 
    public float tiempoRegeneracion = 3.0f; 
    public BarraDeVida barraDeVida;

    private void Start()
    {
        // Iniciar la corrutina de regeneración de vida
        StartCoroutine(RegenerarVida());
        // Inicializar la barra de vida con la vida actual
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
    }

    private IEnumerator RegenerarVida()
    {
        while (true)
        {
            // Esperar el intervalo de tiempo antes de regenerar vida
            yield return new WaitForSeconds(tiempoRegeneracion);

            // Regenerar vida
            if (vida > 0 && vida < vidaMaxima)
            {
                vida += cantidadRegeneracion;

                // Asegurarse de que la vida no exceda la vida máxima
                if (vida > vidaMaxima)
                {
                    vida = vidaMaxima;
                }

                Debug.Log("Jugador ha regenerado vida. Vida actual: " + vida);
                // Actualizar la barra de vida
                barraDeVida.CambiarVidaActual(vida);
            }
        }
    }
}
