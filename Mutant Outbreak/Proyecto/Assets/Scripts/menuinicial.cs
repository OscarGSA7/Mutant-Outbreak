using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuinicial : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("MenuEleccion");
    }

    public void Exit()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
