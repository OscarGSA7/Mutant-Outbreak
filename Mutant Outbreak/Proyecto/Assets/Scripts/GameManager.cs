using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Personaje> personajes;
    private GameObject personajeSeleccionado;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SeleccionarPersonaje(int index)
    {
        personajeSeleccionado = personajes[index].prefab;
    }

    public GameObject ObtenerPersonajeSeleccionado()
    {
        return personajeSeleccionado;
    }
}

[System.Serializable]
public class Personaje
{
    public string nombre;
    public Sprite imagen;
    public GameObject prefab;
}
