using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ControlArma ControlArma;

    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            ControlArma.Disparar();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ControlArma.Recargar();
        }
    }
}
