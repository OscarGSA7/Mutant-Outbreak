using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 6.0f; // Velocidad inicial
    [SerializeField] private Vector2 direccion;
    private Rigidbody2D rb2d;
    private float movimientoX;
    private float movimientoY;
    private Animator animator;
    public BoostVelocidad boostVelocidad;

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

        // Actualizar los parámetros del Animator para reflejar la dirección del movimiento
        animator.SetFloat("MovimientoX", movimientoX);
        animator.SetFloat("MovimientoY", movimientoY);

        // Asegúrate de que 'UltimoX' y 'UltimoY' reflejan la última dirección de movimiento
        if (movimientoX != 0 || movimientoY != 0)
        {
            animator.SetFloat("UltimoX", movimientoX);
            animator.SetFloat("UltimoY", movimientoY);
        }

        direccion = new Vector2(movimientoX, movimientoY).normalized;
        
        if(boostVelocidad.yaUsado == true){
            velocidadMovimiento = 8.0f;
        }
    }

    private void FixedUpdate()
    {
        // Mover el Rigidbody2D en la dirección deseada
        rb2d.MovePosition(rb2d.position + direccion * velocidadMovimiento * Time.fixedDeltaTime); 
    }

    public void IncrementarVelocidad(float cantidad)
    {
        velocidadMovimiento += cantidad; // Aumentar la velocidad en la cantidad dada
    }
     public void SimulateKeyPress(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.W:
                direccion = Vector2.up;
                break;
            case KeyCode.A:
                direccion = Vector2.left;
                break;
            case KeyCode.S:
                direccion = Vector2.down;
                break;
            case KeyCode.D:
                direccion = Vector2.right;
                break;
        }

        animator.SetFloat("MovimientoX", direccion.x);
        animator.SetFloat("MovimientoY", direccion.y);

        rb2d.MovePosition(rb2d.position + direccion * velocidadMovimiento * Time.fixedDeltaTime);
    }
        public void SimulateKeyRelease(KeyCode key)
    {
        // Aquí puedes agregar lógica adicional si necesitas
        direccion = Vector2.zero;
    }
}
