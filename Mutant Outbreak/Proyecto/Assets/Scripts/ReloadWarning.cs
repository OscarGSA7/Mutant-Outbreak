using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadWarning : MonoBehaviour
{
    public Text reloadWarning;
    public ControlArma controlArma;
    public Animator animator;
    public void Start()
    {
        animator = GetComponent<Animator>();
        reloadWarning.gameObject.SetActive(false);
    }

   
    public void Update()
    {
        if(controlArma.currentAmmoInClip < 10){
            reloadWarning.gameObject.SetActive(true);
            animator.SetBool("NeedsToReload", true);
        }
        else
        {
            reloadWarning.gameObject.SetActive(false);
            animator.SetBool("NeedsToReload", false);
        }
    }
}
