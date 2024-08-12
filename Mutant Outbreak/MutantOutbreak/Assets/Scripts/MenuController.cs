using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    private Controles controles;
    [SerializeField] private GameObject opcionesMenu;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject botonPlay;
    [SerializeField] private GameObject botonExit;

    private bool enMenuOpciones = false;

    private void Awake()
    {
        controles = new Controles();
        controles.General.Navegar.performed += ctx => Navegar(ctx.ReadValue<Vector2>());
        controles.General.Atras.performed += ctx => Volver();
        controles.General.Avanzar.performed += ctx => RealizarAccion();
    }

    private void OnEnable()
    {
        controles.Enable();
        EventSystem.current.SetSelectedGameObject(botonPlay.gameObject);
    }

    private void OnDisable()
    {
        controles.Disable();
    }

    private void Start()
    {
        if (opcionesMenu != null)
        {
            opcionesMenu.SetActive(false);
        }

        if (menuPausa != null)
        {
            menuPausa.SetActive(false);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("MenuEleccion");
    }

    public void Exit()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }

    public void ShowOpcionesMenu()
    {
        if (opcionesMenu != null && menuPausa != null)
        {
            menuPausa.SetActive(false);
            opcionesMenu.SetActive(true);
            enMenuOpciones = true;
            SeleccionarPrimerBoton(opcionesMenu);
        }
    }

    public void HideOpcionesMenu()
    {
        if (opcionesMenu != null && menuPausa != null)
        {
            opcionesMenu.SetActive(false);
            menuPausa.SetActive(false);
            enMenuOpciones = false;
            SeleccionarPrimerBoton(menuPausa);
        }
    }

    private void SeleccionarPrimerBoton(GameObject panel)
    {
        if (panel != null)
        {
            Button primerBoton = panel.GetComponentInChildren<Button>();
            if (primerBoton != null)
            {
                EventSystem.current.SetSelectedGameObject(primerBoton.gameObject);
            }
        }
    }

    private void RealizarAccion()
    {
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        if (currentSelected != null)
        {
            Button button = currentSelected.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.Invoke();
            }
        }
    }

    private void Navegar(Vector2 movimiento)
    {
        if (enMenuOpciones) return; // No navegar si estamos en el menú de opciones

        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        if (currentSelected == null) return;

        if (movimiento.y > 0)
        {
            // Navegar hacia el botón anterior (arriba)
            if (currentSelected == botonPlay.gameObject)
            {
                EventSystem.current.SetSelectedGameObject(botonExit.gameObject);
            }
        }
        else if (movimiento.y < 0)
        {
            // Navegar hacia el botón siguiente (abajo)
            if (currentSelected == botonExit.gameObject)
            {
                EventSystem.current.SetSelectedGameObject(botonPlay.gameObject);
            }
        }
    }

    private void Volver()
    {
        if (enMenuOpciones)
        {
            HideOpcionesMenu();
        }
        else
        {
            // Implementar lógica para volver al menú principal o a la pantalla anterior
            // Por ejemplo, si estás en el menú de pausa, podrías volver al juego o al menú principal
            Debug.Log("Tecla de retroceso presionada");
            SceneManager.LoadScene("MainMenu"); // O ajusta esto según tu lógica de menú
        }
    }
}
