using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject opcionesMenu;
    [SerializeField] private GameObject menuPausa; // Referencia al panel del menú de pausa

    private void Start()
    {
        if (opcionesMenu != null)
        {
            opcionesMenu.SetActive(false); // Asegúrate de que el menú de opciones esté desactivado al inicio
        }
        
        if (menuPausa != null)
        {
            menuPausa.SetActive(false); // Asegúrate de que el menú de pausa esté desactivado al inicio
        }
    }

    public void ShowOpcionesMenu()
    {
        if (opcionesMenu != null && menuPausa != null)
        {
            menuPausa.SetActive(false); // Oculta el menú de pausa
            opcionesMenu.SetActive(true); // Muestra el menú de opciones
        }
    }

    public void HideOpcionesMenu()
    {
        if (opcionesMenu != null && menuPausa != null)
        {
            opcionesMenu.SetActive(false); // Oculta el menú de opciones
            menuPausa.SetActive(false); // Oculta el menú de pausa
        }
    }
}
