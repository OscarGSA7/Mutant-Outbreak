using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaZombie : MonoBehaviour
{
    public Slider sliderVida;

    private void Awake()
    {
        if (sliderVida == null)
        {
            sliderVida = GetComponentInChildren<Slider>();
            if (sliderVida == null)
            {
                Debug.LogError("No se encontró un Slider en los hijos de BarraDeVidaZombie.");
            }
        }
    }

    public void ActualizarVida(float vidaActual, float vidaMaxima)
    {
        if (sliderVida != null)
        {
            sliderVida.value = (float)vidaActual / vidaMaxima;
        }
        else
        {
            Debug.LogError("sliderVida no está asignado en BarraDeVidaZombie.");
        }
    }
}
