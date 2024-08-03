using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class VistaPreviaController : MonoBehaviour
{
    [SerializeField] private Dropdown dropdownVistaPrevia;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Text descripcionText;

    [SerializeField] private VideoClip deuteranopiaVideo;
    [SerializeField] private VideoClip protanopiaVideo;
    [SerializeField] private VideoClip tritanopiaVideo;

    private string deuteranopiaDescripcion = "Deuteranopia: Red-green color blindness.";
    private string protanopiaDescripcion = "Protanopia: Red-green color blindness.";
    private string tritanopiaDescripcion = "Tritanopia: Blue-yellow color blindness.";

    private void Start()
    {
        if (dropdownVistaPrevia != null)
        {
            dropdownVistaPrevia.onValueChanged.AddListener(OnDropdownValueChanged);
        }

        videoPlayer.prepareCompleted += OnVideoPrepared;
    }

    private void OnDropdownValueChanged(int value)
    {
        videoPlayer.Stop();
        switch (value)
        {
            case 0:
                videoPlayer.clip = deuteranopiaVideo;
                descripcionText.text = deuteranopiaDescripcion;
                break;
            case 1:
                videoPlayer.clip = protanopiaVideo;
                descripcionText.text = protanopiaDescripcion;
                break;
            case 2:
                videoPlayer.clip = tritanopiaVideo;
                descripcionText.text = tritanopiaDescripcion;
                break;
        }

        videoPlayer.Prepare();
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        vp.time = 0;
        vp.Play();
    }
}
