using UnityEngine;
using System.Collections;

public class GeneradorDeZombies : MonoBehaviour
{
    public GameObject Zombie; 
    public Transform[] puntosDeGeneracion; 
    public int maxZombiesPorRonda = 20; 
    public float cooldownGeneracion = 1.0f; 

    private int zombiesGenerados = 0;

    private void Start()
    {
        if (Zombie == null)
        {
            Debug.LogError("El prefab del zombie no está asignado.");
            return;
        }
        
        if (puntosDeGeneracion == null || puntosDeGeneracion.Length == 0)
        {
            Debug.LogError("No hay puntos de generación asignados.");
            return;
        }

        StartCoroutine(GenerarZombiesConCooldown());
    }

    private IEnumerator GenerarZombiesConCooldown()
    {
        while (zombiesGenerados < maxZombiesPorRonda)
        {
            yield return new WaitForSeconds(cooldownGeneracion);
            GenerarZombie();
        }
    }

    private void GenerarZombie()
    {
        if (Zombie != null && puntosDeGeneracion.Length > 0)
        {
            int indicePunto = Random.Range(0, puntosDeGeneracion.Length);
            Transform puntoDeGeneracion = puntosDeGeneracion[indicePunto];
            Instantiate(Zombie, puntoDeGeneracion.position, puntoDeGeneracion.rotation);
            zombiesGenerados++;
        }
        else
        {
            Debug.LogWarning("Zombie prefab o puntos de generación no están configurados correctamente.");
        }
    }

    
    public void ResetearZombiesGenerados()
    {
        zombiesGenerados = 0;
        StartCoroutine(GenerarZombiesConCooldown());
    }
}
