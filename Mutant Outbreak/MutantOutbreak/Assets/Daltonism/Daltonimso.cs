using UnityEngine;
using UnityEngine.UI;
using Wilberforce;

public class AccesibilidadController : MonoBehaviour
{
    [SerializeField] private Dropdown filtroDropdown;
    [SerializeField] private Button aplicarButton;
    [SerializeField] private Colorblind colorblindEffect;

    private int tipoDeDaltonismoSeleccionado;

    private void Start()
    {
        // Configure the dropdown options
        filtroDropdown.options.Clear();
        filtroDropdown.options.Add(new Dropdown.OptionData() { text = "Ninguno" });
        filtroDropdown.options.Add(new Dropdown.OptionData() { text = "Deuteranopia" });
        filtroDropdown.options.Add(new Dropdown.OptionData() { text = "Protanopia" });
        filtroDropdown.options.Add(new Dropdown.OptionData() { text = "Tritanopia" });

        // Initialize selected type
        tipoDeDaltonismoSeleccionado = colorblindEffect.Type;
        filtroDropdown.value = tipoDeDaltonismoSeleccionado;
        filtroDropdown.RefreshShownValue();

        // Add listener to dropdown and button
        filtroDropdown.onValueChanged.AddListener(delegate { OnDropdownValueChanged(filtroDropdown.value); });
        aplicarButton.onClick.AddListener(ApplyChanges);
    }

    private void OnDropdownValueChanged(int filtroIndex)
    {
        tipoDeDaltonismoSeleccionado = filtroIndex;
    }

    private void ApplyChanges()
    {
        colorblindEffect.Type = tipoDeDaltonismoSeleccionado;
    }
}
