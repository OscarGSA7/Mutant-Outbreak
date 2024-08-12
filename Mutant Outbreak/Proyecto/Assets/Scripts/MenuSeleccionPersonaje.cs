using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuSeleccionPersonaje : MonoBehaviour
{
    private int index;
    [SerializeField] private Image imagen;
    [SerializeField] private Text nombre;
    [SerializeField] private Button botonIniciarJuego; // Agrega esta línea

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        index = PlayerPrefs.GetInt("JugadorIndex");

        if (index > gameManager.personajes.Count - 1)
        {
            index = 0;
        }

        CambiarPantalla(); 

        // Establece el enfoque en el botón "Iniciar Juego"
        if (botonIniciarJuego != null)
        {
            EventSystem.current.SetSelectedGameObject(botonIniciarJuego.gameObject);
        }
    }

    private void CambiarPantalla()
    {
        PlayerPrefs.SetInt("JugadorIndex", index);
        imagen.sprite = gameManager.personajes[index].imagen;
        nombre.text = gameManager.personajes[index].nombre;
    }

    public void SiguientePersonaje()
    {
        if (index == gameManager.personajes.Count - 1)
        {
            index = 0;
        }
        else
        {
            index += 1;
        }
        CambiarPantalla();
    }

    public void AnteriorPersonaje()
    {
        if (index == 0)
        {
            index = gameManager.personajes.Count - 1;
        }
        else
        {
            index -= 1;
        }
        CambiarPantalla();
    }

    public void IniciarJuego()
    {
        gameManager.SeleccionarPersonaje(index);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
