using UnityEngine;
using System.Collections;

public class ControlArma : MonoBehaviour
{
    public AudioSource audioSource; 
    public AudioClip sonidoDisparo;
    public AudioClip sondioRecarga;
    public GameObject balaPrefab; 
    public Transform puntoDisparo; 
    public float velocidadBala = 20f;
    public float tiempoEntreDisparos = 1.0f;
    private float tiempoUltimoDisparo; 

    private float movimientoX;
    private float movimientoY;
    private Animator animator;
    private Vector2 direccion;
    private Vector2 ultimaDireccion; 
    public CameraShake cameraShake;
    public Pausa pausa;

    public int maxAmmoInClip = 30; // Máxima munición en el cartucho
    public int maxAmmoInReserve = 90; 

    public int currentAmmoInClip; // Munición actual en el cartucho
    private int currentAmmoInReserve; // Munición actual en reserva
    public UIController uIController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        ultimaDireccion = Vector2.left; 
        tiempoUltimoDisparo = -tiempoEntreDisparos; 

        // Inicializar la munición
        currentAmmoInClip = maxAmmoInClip;
        currentAmmoInReserve = maxAmmoInReserve;
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

        // Recargar si es necesario
        if (Input.GetKeyDown(KeyCode.R))
        {
            Recargar();
        }
    }

    public void Disparar()
    {
        if (audioSource != null && sonidoDisparo != null && pausa.juegoPausado == false)
        {
            audioSource.PlayOneShot(sonidoDisparo);
        }
        if (currentAmmoInClip > 0 && pausa.juegoPausado == false)
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

            currentAmmoInClip--; // Reducir munición en el cartucho
            UpdateUI();

            StartCoroutine(ResetFiring());
        }
        else
        {
            Debug.Log("Munición en cartucho agotada. Recarga el arma.");
        }
    }

    public void Recargar()
    {
        if (audioSource != null && sondioRecarga != null && pausa.juegoPausado == false && currentAmmoInClip < maxAmmoInClip)
        {
            audioSource.PlayOneShot(sondioRecarga);
        }
        if (currentAmmoInReserve > 0 && pausa.juegoPausado == false && currentAmmoInClip < maxAmmoInClip)
        {
            // Determinar la animación de recarga según la dirección
            if (ultimaDireccion == Vector2.left)
            {
                animator.SetBool("isReloadingLeft", true);
                animator.SetBool("isReloadingRight", false);
            }
            else if (ultimaDireccion == Vector2.right)
            {
                animator.SetBool("isReloadingRight", true);
                animator.SetBool("isReloadingLeft", false);
            }

            StartCoroutine(RecargarCoroutine());
        }
        else
        {
            Debug.Log("Munición en reserva agotada.");
        }
    }


    private IEnumerator RecargarCoroutine()
    {
        // Aquí se asume que la animación de recarga dura 1.5 segundos. Ajusta según sea necesario.
        yield return new WaitForSeconds(1f);

        int ammoNeeded = maxAmmoInClip - currentAmmoInClip;
        int ammoToReload = Mathf.Min(ammoNeeded, currentAmmoInReserve);

        currentAmmoInClip += ammoToReload;
        currentAmmoInReserve -= ammoToReload;

        animator.SetBool("isReloadingLeft", false);
        animator.SetBool("isReloadingRight", false);

        UpdateUI();
    }

    private void UpdateUI()
    {
        uIController.UpdateAmmoUI(currentAmmoInClip, currentAmmoInReserve);
    }

    private IEnumerator ResetFiring()
    {
        yield return new WaitForSeconds(0.01f);
        animator.SetBool("isFiring", false);
    }
    public void RefillAmmo()
    {
        currentAmmoInClip = maxAmmoInClip;
        currentAmmoInReserve = maxAmmoInReserve;
        Debug.Log("Munición repuesta: " + currentAmmoInClip + " / " + currentAmmoInReserve);
        UpdateUI();
    }
}
