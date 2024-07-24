using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;

    private bool isPaused = false;
    private bool juegoPausado = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(juegoPausado)
            {
                Resume();
                }else{
                    pausa();
                }
            
        }
    }

    public void pausa()
    {
        juegoPausado = true;
        if (!isPaused)
        {
            Time.timeScale = 0f;
            botonPausa.SetActive(false);
            menuPausa.SetActive(true);
            isPaused = true;
        }
    }

    public void Resume()
    {
        juegoPausado = false;
        if (isPaused)
        {
            Time.timeScale = 1f;
            botonPausa.SetActive(true);
            menuPausa.SetActive(false);
            isPaused = false;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("menuinicial"); // Cambia "MainMenu" por el nombre de la escena de inicio
    }

    private void OnEnable()
    {
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        isPaused = false;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}