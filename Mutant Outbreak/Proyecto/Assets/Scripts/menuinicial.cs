using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class menuinicial : MonoBehaviour
{
    private Controles controles;
    
    [SerializeField] private Button botonPlay;
    [SerializeField] private Button botonExit;

    private void Awake()
    {
        controles = new Controles();

        controles.General.Avanzar.performed += ctx => RealizarAccion();
        controles.General.Navegar.performed += ctx => Navegar(ctx.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        controles.Enable();

        // Selecciona el primer botón al iniciar
        EventSystem.current.SetSelectedGameObject(botonPlay.gameObject);
    }

    private void OnDisable()
    {
        controles.Disable();
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
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;

        if (movimiento.x > 0)
        {
            // Navegar hacia el siguiente botón (derecha)
            if (currentSelected == botonPlay.gameObject)
            {
                EventSystem.current.SetSelectedGameObject(botonExit.gameObject);
            }
        }
        else if (movimiento.x < 0)
        {
            // Navegar hacia el botón anterior (izquierda)
            if (currentSelected == botonExit.gameObject)
            {
                EventSystem.current.SetSelectedGameObject(botonPlay.gameObject);
            }
        }
    }
}
