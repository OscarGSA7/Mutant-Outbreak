using UnityEngine;
using TMPro;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class ControladorJuego : MonoBehaviour
{
    public Text textoRonda;
    public GameObject prefabZombie;
    public GameObject prefabBarraDeVidaZombie;
    public int zombiesPorRondaInicial = 4;
    public float tiempoEntreRondas = 10.0f;
    public float cooldownEntreZombies = 1.0f;
    public Vector3[] posicionesZombies;

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
                zombiesPorRondaInicial += 3;
                ActualizarTextoRonda();
                StartCoroutine(GenerarZombiesConCooldown());

                cambiandoDeRonda = false;
            }
        }
    }

    private void ActualizarTextoRonda()
    {
        textoRonda.text = "" + rondaActual;
    }

    private IEnumerator GenerarZombiesConCooldown()
    {
        for (int i = 0; i < zombiesPorRondaInicial; i++)
        {
            int randomIndex = Random.Range(0, posicionesZombies.Length);
            Vector2 posicion = posicionesZombies[randomIndex];
            GameObject zombie = Instantiate(prefabZombie, posicion, Quaternion.identity);

            GameObject barra = Instantiate(prefabBarraDeVidaZombie, zombie.transform.position + new Vector3(0, 1, 0), Quaternion.identity, zombie.transform);
            BarraDeVidaZombie barraDeVida = barra.GetComponent<BarraDeVidaZombie>();

            Enemy zombieScript = zombie.GetComponent<Enemy>();
            if (zombieScript != null)
            {
                zombieScript.prefabBarraDeVidaZombi = prefabBarraDeVidaZombie;
                zombieScript.barraDeVida = barraDeVida;
                zombieScript.AjustarVidaPorRonda(rondaActual); // Ajusta la vida del zombie según la ronda actual
            }

            yield return new WaitForSeconds(cooldownEntreZombies);
        }
    }

    private bool HayZombiesEnPartida()
    {
        return GameObject.FindGameObjectsWithTag("Enemigo").Any(zombie => zombie.activeSelf);
    }
}
