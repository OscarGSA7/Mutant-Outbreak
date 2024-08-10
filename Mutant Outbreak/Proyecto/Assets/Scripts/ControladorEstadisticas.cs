using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControladorEstadisticas : MonoBehaviour
{
    public Text estadisticasTexto; 
    private bool estadisticasVisibles = false; 
    private float tiempoJugado = 0f; 
    private int zombiesEliminados = 0; 
    private int puntosJugador = 0; 

    private void Start()
    {
        
        estadisticasTexto.text = "";
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            estadisticasVisibles = !estadisticasVisibles;
            ActualizarTextoEstadisticas();
        }

        
        if (estadisticasVisibles)
        {
            tiempoJugado += Time.deltaTime;
            ActualizarTextoEstadisticas();
        }
    }

    private void ActualizarTextoEstadisticas()
    {
        
        if (estadisticasVisibles)
        {
            estadisticasTexto.text = "Estadísticas de la partida:\n" +
                "Zombies eliminados: " + zombiesEliminados + "\n" +
                "Puntos del jugador: " + puntosJugador + "\n" +
                "Tiempo jugado: " + tiempoJugado.ToString("F1") + " segundos";
        }
        else
        {
            estadisticasTexto.text = "";
        }
    }

    public void RegistrarZombieEliminado()
    {
        
        zombiesEliminados++;
        puntosJugador += 100;
        ActualizarTextoEstadisticas();
    }
}
