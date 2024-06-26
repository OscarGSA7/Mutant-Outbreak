using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cambiarescena2 : MonoBehaviour
{
    
    public void CambiaraAEscena2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CambiarAEscena1(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }
    public void CambiarAEscena3(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void CambiarAEscena4(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
