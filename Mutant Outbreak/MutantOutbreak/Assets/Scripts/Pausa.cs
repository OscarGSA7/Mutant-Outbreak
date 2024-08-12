using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class Pausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject panelOpciones;
    [SerializeField] private GameObject panelPantalla;
    [SerializeField] private GameObject panelSonido;
    [SerializeField] private GameObject panelControles;
    [SerializeField] private GameObject panelAccesibilidad;
    [SerializeField] private Button botonOpciones;
    [SerializeField] private Button tabPantalla;
    [SerializeField] private Button tabSonido;
    [SerializeField] private Button tabControles;
    [SerializeField] private Button tabAccesibilidad;
    [SerializeField] private Button botonVolver; 

    private Controles controles;
    private bool isPaused = false;
    public bool juegoPausado = false;

    private void Awake()
    {
        controles = new Controles();
        controles.General.Pausa.performed += ctx => PausarOContinuar();
        controles.General.Atras.performed += ctx => RegresarMenuPausa();
        controles.General.Avanzar.performed += ctx => RealizarAccion();
    }

    private void OnEnable()
    {
        controles.Enable();
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        panelOpciones.SetActive(false);
        isPaused = false;
    }

    private void OnDisable()
    {
        controles.Disable();
        Time.timeScale = 1f;
    }

    private void Start()
    {
        botonOpciones.onClick.AddListener(MostrarOpciones);
        botonVolver.onClick.AddListener(Resume); 

        tabPantalla.onClick.AddListener(() => MostrarPanel(panelPantalla));
        tabSonido.onClick.AddListener(() => MostrarPanel(panelSonido));
        tabControles.onClick.AddListener(() => MostrarPanel(panelControles));
        tabAccesibilidad.onClick.AddListener(() => MostrarPanel(panelAccesibilidad));
    }

    private void PausarOContinuar()
    {
        Debug.Log("PausarOContinuar() llamado. Juego pausado: " + juegoPausado);

        if (panelOpciones.activeSelf)
        {
            Debug.Log("Regresando al menú de pausa desde las opciones.");
            RegresarMenuPausa();
        }
        else if (juegoPausado)
        {
            Debug.Log("Reanudando el juego.");
            Resume();
        }
        else
        {
            Debug.Log("Pausando el juego.");
            Pausar();
        }
    }

    private void Pausar()
    {
        juegoPausado = true;
        if (!isPaused)
        {
            Time.timeScale = 0f;
            botonPausa.SetActive(false);
            menuPausa.SetActive(true);
            isPaused = true;
            EventSystem.current.SetSelectedGameObject(botonOpciones.gameObject);
        }
    }

    public void Resume()
    {
        juegoPausado = false;
        if (isPaused)
        {
            Time.timeScale = 1f;
            botonPausa.SetActive(true);
            menuPausa.SetActive(false);
            panelOpciones.SetActive(false);
            isPaused = false;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void MostrarOpciones()
    {
        menuPausa.SetActive(false);
        panelOpciones.SetActive(true);
        EventSystem.current.SetSelectedGameObject(tabPantalla.gameObject);
    }

    private void RegresarMenuPausa()
    {
        if (panelOpciones.activeSelf)
        {
            panelOpciones.SetActive(false);
            menuPausa.SetActive(true);
            EventSystem.current.SetSelectedGameObject(botonOpciones.gameObject);
        }
        else
        {
            Resume();
        }
    }

    private void MostrarPanel(GameObject panel)
    {
        panelPantalla.SetActive(false);
        panelSonido.SetActive(false);
        panelControles.SetActive(false);
        panelAccesibilidad.SetActive(false);
        panel.SetActive(true);

        SelectFirstInteractable(panel);
    }

private void SelectFirstInteractable(GameObject panel)
{
    Selectable firstInteractable = panel.GetComponentInChildren<Selectable>();

    if (firstInteractable != null)
    {
        EventSystem.current.SetSelectedGameObject(firstInteractable.gameObject);

        Dropdown dropdown = firstInteractable.GetComponent<Dropdown>();
        if (dropdown != null)
        {
            Debug.Log("Dropdown encontrado: " + dropdown.name);

            if (dropdown.options != null && dropdown.options.Count > 0)
            {
                Debug.Log("Dropdown tiene opciones disponibles.");

            }
            else
            {
                Debug.LogError("Dropdown no tiene opciones disponibles.");
            }
        }
        else
        {
            Debug.LogError("El objeto interactivo no es un Dropdown.");
        }
    }
    else
    {
        Debug.LogError("No se encontró un objeto interactivo dentro del panel.");
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
                return;
            }

            Dropdown dropdown = currentSelected.GetComponent<Dropdown>();
            if (dropdown != null)
            {
                dropdown.Show();
            }
        }
    }
}
