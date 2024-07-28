using UnityEngine;

public class Bala : MonoBehaviour
{
    public int daño = 50; 
    public float velocidadBala = 10.0f; 
    private Vector2 direccion;

    public void DireccionDisparo(Vector2 direccion)
    {
        this.direccion = direccion.normalized; 
    }

    private void Update()
    {
        transform.Translate(direccion * velocidadBala * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            Enemy enemigoScript = other.GetComponent<Enemy>();
            if (enemigoScript != null)
            {
                enemigoScript.RecibirDaño(daño);
            }
            Destroy(gameObject); 
        }
    }
}
