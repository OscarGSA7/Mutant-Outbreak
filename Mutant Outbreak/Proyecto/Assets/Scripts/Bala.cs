using UnityEngine;

public class Bala : MonoBehaviour
{
    public int daño = 20; // Daño que la bala causa
    public float velocidadBala = 10.0f; // Velocidad de la bala
    private Vector2 direccion;

    public void DireccionDisparo(Vector2 direccion)
    {
        this.direccion = direccion.normalized; // Normaliza la dirección de disparo
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
            Destroy(gameObject); // Destruir la bala al impactar
        }
    }
}
