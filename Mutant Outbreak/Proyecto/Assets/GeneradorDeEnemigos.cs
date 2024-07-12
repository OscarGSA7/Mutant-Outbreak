using UnityEngine;
using System.Collections;

public class GeneradorDeZombies : MonoBehaviour
{
    public GameObject zombiePrefab; 
    public Transform[] puntosDeGeneracion; 
    public int maxZombiesPorRonda = 20; 
    public float cooldown = 1.0f; 

    private int zombiesGenerados = 0; 

    private void Start()
    {
        
        StartCoroutine(GenerarZombiesConCooldown());
    }

    private IEnumerator GenerarZombiesConCooldown()
    {
        while (zombiesGenerados < maxZombiesPorRonda)
        {
            GenerarZombie();
            yield return new WaitForSeconds(cooldown); 
        }
    }

    private void GenerarZombie()
    {
        if (zombiesGenerados < maxZombiesPorRonda)
        {
            
            Transform puntoDeGeneracion = puntosDeGeneracion[Random.Range(0, puntosDeGeneracion.Length)];

            
            Instantiate(zombiePrefab, puntoDeGeneracion.position, puntoDeGeneracion.rotation);

            
            zombiesGenerados++;
        }
    }

    
    public void ReiniciarGenerador()
    {
        zombiesGenerados = 0;
        
        StartCoroutine(GenerarZombiesConCooldown());
    }
}
