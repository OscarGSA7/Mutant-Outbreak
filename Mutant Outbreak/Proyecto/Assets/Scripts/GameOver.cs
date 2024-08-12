using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class GameOver : MonoBehaviour
{
    private Controles controles;
    
    [SerializeField] private Button botonRestart;
    [SerializeField] private Button botonRegresar;

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
        EventSystem.current.SetSelectedGameObject(botonRestart.gameObject);
    }

    private void OnDisable()
    {
        controles.Disable();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Regresar()
    {
        SceneManager.LoadScene("MainMenu");
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
            if (currentSelected == botonRestart.gameObject)
            {
                EventSystem.current.SetSelectedGameObject(botonRegresar.gameObject);
            }
        }
        else if (movimiento.x < 0)
        {
            // Navegar hacia el botón anterior (izquierda)
            if (currentSelected == botonRegresar.gameObject)
            {
                EventSystem.current.SetSelectedGameObject(botonRestart.gameObject);
            }
        }
    }
}
