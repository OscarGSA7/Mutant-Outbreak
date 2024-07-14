using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento; 
    [SerializeField] private Vector2 direccion;
    private Rigidbody2D rb2d;
    private float movimientoX;
    private float movimientoY;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        
        if (Input.GetKey(KeyCode.Q))
        {
            velocidadMovimiento = 0.5f;
        }
        else
        {
            velocidadMovimiento = 6.0f; 
        }

        movimientoX = Input.GetAxisRaw("Horizontal");
        movimientoY = Input.GetAxisRaw("Vertical");

        animator.SetFloat("MovimientoX", movimientoX);
        animator.SetFloat("MovimientoY", movimientoY);
        if (movimientoX != 0 || movimientoY != 0)
        {
            animator.SetFloat("UltimoX", movimientoX);
            animator.SetFloat("UltimoY", movimientoY);
        }

        direccion = new Vector2(movimientoX, movimientoY).normalized;
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + direccion * velocidadMovimiento * Time.fixedDeltaTime); 
    }
}
