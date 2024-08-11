using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Slider slider;
    public Text vidaText; // Referencia al componente Text

    public void CambiarVidaMaxima(float vidaMaxima)
    {
        slider.maxValue = vidaMaxima;
        ActualizarTextoDeVida(vidaMaxima);
    }

    public void InicializarBarraDeVida(float cantidadVida)
    {
        CambiarVidaMaxima(cantidadVida);
        slider.value = cantidadVida;
        ActualizarTextoDeVida(cantidadVida);
    }

    public void CambiarVidaActual(float vida)
    {
        slider.value = vida;
        ActualizarTextoDeVida(vida);
    }

    private void ActualizarTextoDeVida(float vida)
    {
        vidaText.text = $"{vida}";
    }
}
