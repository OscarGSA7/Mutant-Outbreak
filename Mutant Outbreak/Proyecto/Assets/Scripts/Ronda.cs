using UnityEngine;
using TMPro;
using System.Collections;
using System.Linq;

public class ControladorJuego : MonoBehaviour
{
    public TextMeshProUGUI textoRonda;
    public GameObject prefabZombie;
    public int zombiesPorRondaInicial = 4;
    public float tiempoEntreRondas = 10.0f;
    public float cooldownEntreZombies = 1.0f;

    private int rondaActual = 0;
    private bool cambiandoDeRonda = false;

    private void Start()
    {
        ActualizarTextoRonda();
        StartCoroutine(VerificarFinDeRonda());
    }

    private IEnumerator VerificarFinDeRonda()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            if (!HayZombiesEnPartida() && !cambiandoDeRonda)
            {
                cambiandoDeRonda = true;
                yield return new WaitForSeconds(tiempoEntreRondas);

                rondaActual++;
                zombiesPorRondaInicial += 4;
                ActualizarTextoRonda();
                SpawnearZombies();

                cambiandoDeRonda = false;
            }
        }
    }

    private void ActualizarTextoRonda()
    {
        textoRonda.text = "" + rondaActual;
    }

    private void SpawnearZombies()
    {
        StartCoroutine(GenerarZombiesConCooldown());
    }

    private IEnumerator GenerarZombiesConCooldown()
    {
        for (int i = 0; i < zombiesPorRondaInicial; i++)
        {
            Vector3 randomPosition = GetRandomSpawnPosition();
            Instantiate(prefabZombie, randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(cooldownEntreZombies);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(-10f, 10f);
        return new Vector3(randomX, randomY, 0f);
    }

    private bool HayZombiesEnPartida()
    {
        return GameObject.FindGameObjectWithTag("Enemigo") != null;
    }
}
