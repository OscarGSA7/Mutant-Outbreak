using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int vida = 100;

    public void TomarDaño(int daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        
        Destroy(gameObject);
    }
}
