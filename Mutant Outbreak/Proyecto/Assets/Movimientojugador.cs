using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento; 
    [SerializeField] private Vector2 direccion;
    private Rigidbody2D rb2d;

    private void Start() {
        rb2d = GetComponent<Rigidbody2D>(); 
    }

    private void Update() {
        direccion = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void FixedUpdate() {
        rb2d.MovePosition(rb2d.position + direccion * velocidadMovimiento * Time.fixedDeltaTime); 
    }
}
