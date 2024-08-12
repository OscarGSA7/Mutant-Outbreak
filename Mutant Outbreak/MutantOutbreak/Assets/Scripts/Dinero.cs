using UnityEngine;
using UnityEngine.UI;

public class ControladorDinero : MonoBehaviour
{
    public int cantidad;
    public Text textoDinero;

    private void Start()
    {
        ActualizarTextoDinero();
    }

    public void AgregarDinero(int cantidad)
    {
        this.cantidad += cantidad;
        ActualizarTextoDinero();
    }

    public bool QuitarDinero(int cantidad)
    {
        if (this.cantidad >= cantidad)
        {
            this.cantidad -= cantidad;
            ActualizarTextoDinero();
            return true;
        }
        else
        {
            Debug.Log("No tienes suficientes puntos para realizar esta acción.");
            return false;
        }
    }

    private void ActualizarTextoDinero()
    {
        if (textoDinero != null)
        {
            textoDinero.text = "$" + this.cantidad.ToString();
        }
    }
}
