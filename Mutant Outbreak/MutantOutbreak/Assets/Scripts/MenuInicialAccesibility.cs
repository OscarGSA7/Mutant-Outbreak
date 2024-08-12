using UnityEngine;
using UnityEngine.UI;
using Wilberforce;

public class PanelAccesibilidadController : MonoBehaviour
{
    [SerializeField] private Dropdown dropdownDaltonismo;
    [SerializeField] private Button aplicarButton;
    [SerializeField] private Button volverButton;
    [SerializeField] private GameObject panelAccesibilidad;
    [SerializeField] private GameObject menuInicial;
    [SerializeField] private Colorblind colorblind;

    private int tipoDaltonismoActual;

    private void Start()
    {
        if (dropdownDaltonismo != null)
        {
            dropdownDaltonismo.onValueChanged.AddListener(OnDropdownValueChanged);
        }

        if (aplicarButton != null)
        {
            aplicarButton.onClick.AddListener(AplicarCambios);
        }

        if (volverButton != null)
        {
            volverButton.onClick.AddListener(Volver);
        }

        panelAccesibilidad.SetActive(false); // Asegúrate de que el panel de accesibilidad esté desactivado al inicio
    }

    private void OnDropdownValueChanged(int value)
    {
        tipoDaltonismoActual = value;
    }

    public void AplicarCambios()
    {
        if (colorblind != null)
        {
            colorblind.Type = tipoDaltonismoActual;
        }
    }

    private void Volver()
    {
        panelAccesibilidad.SetActive(false);
        menuInicial.SetActive(true);
    }

    public void MostrarPanelAccesibilidad()
    {
        menuInicial.SetActive(false);
        panelAccesibilidad.SetActive(true);
    }
}
