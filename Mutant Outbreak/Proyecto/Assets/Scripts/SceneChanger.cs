using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        // Guardar el nombre de la escena a cargar
        PlayerPrefs.SetString("Game", sceneName);
        
        // Cargar la escena de carga
        SceneManager.LoadScene("LoadingScene");
    }
}
