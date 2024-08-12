using UnityEngine;

public class Bala : MonoBehaviour
{
    public float daño = 20; 
    public float velocidadBala = 10.0f; 
    private Vector2 direccion;
    private ControladorDinero controladorDinero;
    private Enemy enemigo;
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
            Debug.Log("Bala impactó al zombie. Aplicando daño: " + daño);
            enemigoScript.RecibirDaño((int)daño);
            Destroy(gameObject); 
            controladorDinero.AgregarDinero(10); 
        }
        else
        {
            
        }
        Destroy(gameObject);
    }
}

}
