using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public Slider progressBar;
    public Text progressText;
    public GameObject videoPlayer; // Asigna tu VideoPlayer o Animator aquí

    private void Start()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        // Obtén el nombre de la escena a cargar desde PlayerPrefs
        string sceneToLoad = PlayerPrefs.GetString("SceneToLoad", "Game");

        // Inicia la carga de la escena en segundo plano
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false; // Desactivar la activación automática

        float startTime = Time.time;

        // Mientras la escena se esté cargando
        while (!operation.isDone)
        {
            // Calcula el progreso (entre 0 y 1)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // Actualiza la barra de progreso y el texto
            progressBar.value = progress;
            progressText.text = (progress * 100f).ToString("F0") + "%";

            // Esperar al menos 10 segundos antes de activar la escena
            if (operation.progress >= 0.9f && Time.time >= startTime + 10.0f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
