using UnityEngine;
using TMPro;
 
public class SumaRestaBotones : MonoBehaviour
{
    public TMP_Text textoResultado;
    private int resultado = 0;
 
    public void SumarCinco()
    {
        resultado += 5;
        ActualizarTexto();
    }
 
    public void RestarUno()
    {
        resultado -= 1;
        ActualizarTexto();
    }
 
    private void ActualizarTexto()
    {
        textoResultado.text = "Resultado: " + resultado;
    }
}
 