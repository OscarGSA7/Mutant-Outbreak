using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Text ammoText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateAmmoUI(int ammoInClip, int ammoInReserve)
    {
        ammoText.text = $"{ammoInClip}/{ammoInReserve}";
    }


}
