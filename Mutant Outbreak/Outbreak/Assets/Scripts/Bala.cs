using UnityEngine;

public class Bala : MonoBehaviour
{
    public int daño = 50; 
    public float velocidadBala = 10.0f; 
    private Vector2 direccion;
    private ControladorDinero controladorDinero;
    public void Start(){
        controladorDinero = FindObjectOfType<ControladorDinero>();
    }
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
            controladorDinero.AgregarDinero(10); // Sumar 10 puntos por impacto
        }
        else
        {
            
        }
        Destroy(gameObject);
    }
}

}
