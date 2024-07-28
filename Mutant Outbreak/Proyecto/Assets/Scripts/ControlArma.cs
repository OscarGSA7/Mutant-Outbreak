using UnityEngine;
using System.Collections;

public class ControlArma : MonoBehaviour
{
    public GameObject balaPrefab; // Prefab de la bala
    public Transform puntoDisparo; // Punto desde donde se disparan las balas
    public float velocidadBala = 20f;
    public float tiempoEntreDisparos = 1.0f; // Tiempo entre disparos
    private float tiempoUltimoDisparo; // Tiempo del último disparo

    private float movimientoX;
    private float movimientoY;
    private Animator animator;
    private Vector2 direccion;
    private Vector2 ultimaDireccion; 
    public CameraShake cameraShake;

    private void Start()
    {
        animator = GetComponent<Animator>();
        ultimaDireccion = Vector2.right; 
        tiempoUltimoDisparo = -tiempoEntreDisparos; 

        
        
    }

    private void Update()
    {
        direccion = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direccion = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direccion = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direccion = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direccion = Vector2.right;
        }

        movimientoX = Input.GetAxisRaw("Horizontal");
        movimientoY = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movimientoX);
        animator.SetFloat("Vertical", movimientoY);

        if (movimientoX != 0 || movimientoY != 0)
        {
            ultimaDireccion = new Vector2(movimientoX, movimientoY).normalized;
            animator.SetFloat("UltimoHorizontal", movimientoX);
            animator.SetFloat("UltimoVertical", movimientoY);
        }

        if (direccion != Vector2.zero)
        {
            ultimaDireccion = direccion; // Actualizar última dirección solo si se está moviendo
        }

        if (Input.GetMouseButtonDown(0) && Time.time >= tiempoUltimoDisparo + tiempoEntreDisparos)
        {
            Disparar();
        }

        // Actualizar las animaciones de disparo
        animator.SetFloat("Horizontal", ultimaDireccion.x);
        animator.SetFloat("Vertical", ultimaDireccion.y);
    }

    private void Disparar()
    {
        animator.SetBool("isFiring", true);

        GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
        Bala balaScript = bala.GetComponent<Bala>();
        if (balaScript != null)
        {
            balaScript.DireccionDisparo(ultimaDireccion);
            balaScript.daño = 20; 
        }

        if (cameraShake != null)
        {
            cameraShake.ShakeCamera(2f, 0.1f); 
        }

        tiempoUltimoDisparo = Time.time; // Actualizar el tiempo del último disparo

        StartCoroutine(ResetFiring());
    }

    private IEnumerator ResetFiring()
    {
        yield return new WaitForSeconds(0.01f);
        animator.SetBool("isFiring", false);
    }
}
