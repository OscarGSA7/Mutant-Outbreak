using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private int dañoGolpe = 20;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;
    private Animator animator;

    private void Start(){
        animator = GetComponent<Animator>(); 
    }
    private void Update()
    {
        if (tiempoSiguienteAtaque > 0){
            tiempoSiguienteAtaque -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E) && tiempoSiguienteAtaque <= 0){
            Golpe();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
    }

    
    private void Golpe()
    {
        animator.SetTrigger("Golpe");
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        foreach (Collider2D colisionador in objetos){
            if(colisionador.CompareTag("Enemigo")){
                colisionador.transform.GetComponent<Enemy>().RecibirDaño(dañoGolpe);
            }
        }
    }
     private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}

