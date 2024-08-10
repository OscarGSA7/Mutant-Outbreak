using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transicion : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AnimationClip animacionfinal;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            StartCoroutine(CambiarEscena());
        }
    }
    IEnumerator CambiarEscena(){
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animacionfinal.length);
        SceneManager.LoadScene("MainMenu");
    }
}
