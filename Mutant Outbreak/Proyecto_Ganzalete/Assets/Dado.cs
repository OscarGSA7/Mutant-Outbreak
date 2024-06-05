using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Importar el namespace de TextMeshPro
using System.Collections.Generic;  // Importar el namespace para usar listas

public class LanzarDado : MonoBehaviour
{
    public TMP_Text[] posiciones; // Array de textos para mostrar los números del dado
    public Button botonLanzar;    // Referencia al botón para lanzar el dado

    void Start()
    {
        // Asegurarse de que las referencias están asignadas
        if (posiciones == null || posiciones.Length != 6)
        {
            Debug.LogError("Asignar correctamente los objetos de texto en el inspector.");
            return;
        }

        if (botonLanzar == null)
        {
            Debug.LogError("Asignar el botón en el inspector.");
            return;
        }

        // Añadir listener al botón
        botonLanzar.onClick.AddListener(Lanzar);
    }

    // Método para mover Mono a una posición aleatoria
    public void Lanzar()
    {
        // Crear una lista de números del 1 al 6
        List<int> numeros = new List<int> { 1, 2, 3, 4, 5, 6 };

        // Mezclar los números aleatoriamente usando Fisher-Yates shuffle
        for (int i = 0; i < numeros.Count; i++)
        {
            int randomIndex = Random.Range(i, numeros.Count);
            int temp = numeros[i];
            numeros[i] = numeros[randomIndex];
            numeros[randomIndex] = temp;
        }

        // Asignar los números mezclados a las posiciones de texto
        for (int i = 0; i < posiciones.Length; i++)
        {
            posiciones[i].text = numeros[i].ToString();
        }
    }
}
