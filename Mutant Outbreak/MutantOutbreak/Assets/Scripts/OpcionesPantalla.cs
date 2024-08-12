using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpcionesPantalla : MonoBehaviour
{
    [SerializeField] private Dropdown resolucionDropdown;
    [SerializeField] private Dropdown modoVisualizacionDropdown;
    [SerializeField] private Dropdown tasaActualizacionDropdown;
    [SerializeField] private Button aplicarButton;
    [SerializeField] private Button revertirButton;

    private Resolution[] resoluciones;
    private int resolucionActualIndex;
    private FullScreenMode[] modosVisualizacion = { FullScreenMode.FullScreenWindow, FullScreenMode.Windowed };
    private int modoVisualizacionActualIndex;
    private List<int> tasasActualizacion = new List<int>();

    private int resolucionInicialIndex;
    private int modoVisualizacionInicialIndex;
    private int tasaActualizacionInicialIndex;

    private bool panelActivo = false; // Variable para verificar si el panel está activo

    [System.Obsolete]
    private void Start()
    {
        resoluciones = Screen.resolutions;
        InicializarDropdowns();
        StoreInitialSettings();

        aplicarButton.onClick.AddListener(AplicarCambios);
        revertirButton.onClick.AddListener(RevertirCambios);

        // Inicialmente, el panel está cerrado
        EnableKeyboardShortcuts(false);
    }

    private void Update()
    {
        // Detectar si el panel está activo y escuchar las teclas 'R' y 'F'
        if (panelActivo)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SimularClic(revertirButton);
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                SimularClic(aplicarButton);
            }
        }
    }

    [System.Obsolete]
    private void InicializarDropdowns()
    {
        resolucionDropdown.ClearOptions();
        List<string> opcionesResolucion = new List<string>();
        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            opcionesResolucion.Add(opcion);
            if (resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActualIndex = i;
            }
        }
        resolucionDropdown.AddOptions(opcionesResolucion);
        resolucionDropdown.value = resolucionActualIndex;
        resolucionDropdown.RefreshShownValue();

        modoVisualizacionDropdown.ClearOptions();
        List<string> opcionesModoVisualizacion = new List<string> { "Pantalla Completa", "Ventana" };
        modoVisualizacionDropdown.AddOptions(opcionesModoVisualizacion);
        modoVisualizacionDropdown.value = (int)Screen.fullScreenMode;
        modoVisualizacionDropdown.RefreshShownValue();

        tasaActualizacionDropdown.ClearOptions();
        List<string> opcionesTasaActualizacion = new List<string>();
        for (int i = 0; i < resoluciones.Length; i++)
        {
            if (resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            {
                if (!tasasActualizacion.Contains(resoluciones[i].refreshRate))
                {
                    tasasActualizacion.Add(resoluciones[i].refreshRate);
                    opcionesTasaActualizacion.Add(resoluciones[i].refreshRate + " Hz");
                }
            }
        }
        tasaActualizacionDropdown.AddOptions(opcionesTasaActualizacion);
        tasaActualizacionDropdown.value = tasasActualizacion.IndexOf(Screen.currentResolution.refreshRate);
        tasaActualizacionDropdown.RefreshShownValue();
    }

    private void StoreInitialSettings()
    {
        resolucionInicialIndex = resolucionDropdown.value;
        modoVisualizacionInicialIndex = modoVisualizacionDropdown.value;
        tasaActualizacionInicialIndex = tasaActualizacionDropdown.value;
    }

    [System.Obsolete]
    public void AplicarCambios()
    {
        Resolution resolucionSeleccionada = resoluciones[resolucionDropdown.value];
        int tasaActualizacionSeleccionada = tasasActualizacion[tasaActualizacionDropdown.value];
        FullScreenMode modoVisualizacionSeleccionado = modosVisualizacion[modoVisualizacionDropdown.value];

        Screen.SetResolution(resolucionSeleccionada.width, resolucionSeleccionada.height, modoVisualizacionSeleccionado, tasaActualizacionSeleccionada);
    }

    [System.Obsolete]
    public void RevertirCambios()
    {
        resolucionDropdown.value = resolucionInicialIndex;
        modoVisualizacionDropdown.value = modoVisualizacionInicialIndex;
        tasaActualizacionDropdown.value = tasaActualizacionInicialIndex;

        AplicarCambios(); 
    }

    public void EnableKeyboardShortcuts(bool enable)
    {
        panelActivo = enable;
    }

    public void AbrirPanel()
    {
        // Mostrar el panel
        EnableKeyboardShortcuts(true);
    }

    public void CerrarPanel()
    {
        // Ocultar el panel
        EnableKeyboardShortcuts(false);
    }

    private void SimularClic(Button boton)
    {
        boton.onClick.Invoke();
    }
}
