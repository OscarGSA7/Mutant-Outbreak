using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Asegúrate de incluir esta línea

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

    private Controles controles;
    private bool isPaused = false;
    public bool juegoPausado = false;

    private void Awake()
    {
        controles = new Controles();
        controles.General.Pausa.performed += ctx => PausarOContinuar();
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
        panelOpciones.SetActive(false);
        panelPantalla.SetActive(true); 
        panelSonido.SetActive(false);
        panelControles.SetActive(false);
        panelAccesibilidad.SetActive(false);

        botonOpciones.onClick.AddListener(MostrarOpciones);
        tabPantalla.onClick.AddListener(() => MostrarPanel(panelPantalla));
        tabSonido.onClick.AddListener(() => MostrarPanel(panelSonido));
        tabControles.onClick.AddListener(() => MostrarPanel(panelControles));
        tabAccesibilidad.onClick.AddListener(() => MostrarPanel(panelAccesibilidad));
    }

    private void PausarOContinuar()
    {
        if (panelOpciones.activeSelf)
        {
            RegresarMenuPausa();
        }
        else if (juegoPausado)
        {
            Resume();
        }
        else
        {
            pausa();
        }
    }

    private void pausa()
    {
        juegoPausado = true;
        if (!isPaused)
        {
            Time.timeScale = 0f;
            botonPausa.SetActive(false);
            menuPausa.SetActive(true);
            isPaused = true;
        }
    }

    private void Resume()
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
    }

    private void RegresarMenuPausa()
    {
        panelOpciones.SetActive(false);
        menuPausa.SetActive(true);
    }

    private void MostrarPanel(GameObject panel)
    {
        panelPantalla.SetActive(false);
        panelSonido.SetActive(false);
        panelControles.SetActive(false);
        panelAccesibilidad.SetActive(false);

        panel.SetActive(true);
    }

    public void HideOpcionesMenu()
    {
        if (panelOpciones != null && menuPausa != null)
        {
            panelOpciones.SetActive(false); // Oculta el menú de opciones
            menuPausa.SetActive(false); // Oculta el menú de pausa
            Resume(); // Resuma el juego
        }
    }
}
