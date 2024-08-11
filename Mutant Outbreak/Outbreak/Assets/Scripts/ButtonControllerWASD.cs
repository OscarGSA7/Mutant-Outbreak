using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour
{
    public Button buttonW;
    public Button buttonA;
    public Button buttonS;
    public Button buttonD;
    public Button buttonShoot;
    public Button buttonReload;
    public Button buttonInteract;
    private Movimiento movimientoScript;
    private ControlArma shootingScript;


    private bool isPressingW;
    private bool isPressingA;
    private bool isPressingS;
    private bool isPressingD;


    private void Start()
    {
        movimientoScript = FindObjectOfType<Movimiento>();
        shootingScript = FindObjectOfType<ControlArma>();

        // Asignar listeners a los botones
        AddButtonListeners(buttonW, "W");
        AddButtonListeners(buttonA, "A");
        AddButtonListeners(buttonS, "S");
        AddButtonListeners(buttonD, "D");
        AddButtonListeners(buttonInteract,"F");

        // Asignar listener al botón de disparo y recarga
        AddShootButtonListeners(buttonShoot, OnShootButtonPress);
        AddShootButtonListeners(buttonReload, OnReloadButtonPress);
    }

    private void AddButtonListeners(Button button, string key)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = button.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entryPointerDown = new EventTrigger.Entry();
        entryPointerDown.eventID = EventTriggerType.PointerDown;
        entryPointerDown.callback.AddListener((data) => { OnButtonPress(key); });
        trigger.triggers.Add(entryPointerDown);

        EventTrigger.Entry entryPointerUp = new EventTrigger.Entry();
        entryPointerUp.eventID = EventTriggerType.PointerUp;
        entryPointerUp.callback.AddListener((data) => { OnButtonRelease(key); });
        trigger.triggers.Add(entryPointerUp);
    }

    private void AddShootButtonListeners(Button button, System.Action action)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = button.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entryPointerDown = new EventTrigger.Entry();
        entryPointerDown.eventID = EventTriggerType.PointerDown;
        entryPointerDown.callback.AddListener((data) => { action.Invoke(); });
        trigger.triggers.Add(entryPointerDown);
    }

    public void OnButtonPress(string key)
    {
        switch (key)
        {
            case "W":
                isPressingW = true;
                break;
            case "A":
                isPressingA = true;
                break;
            case "S":
                isPressingS = true;
                break;
            case "D":
                isPressingD = true;
                break;
        }
    }

    public void OnButtonRelease(string key)
    {
        switch (key)
        {
            case "W":
                isPressingW = false;
                break;
            case "A":
                isPressingA = false;
                break;
            case "S":
                isPressingS = false;
                break;
            case "D":
                isPressingD = false;
                break;
        }
    }

    public void OnShootButtonPress()
    {
        shootingScript.Disparar();
        Debug.Log("Disparando");
    }

    public void OnReloadButtonPress()
    {
        shootingScript.Recargar();
        Debug.Log("Recargando");
    }

    private void Update()
    {

        if (isPressingW)
        {
            movimientoScript.SimulateKeyPress(KeyCode.W);
            Debug.Log("w");
        }
        if (isPressingA)
        {
            movimientoScript.SimulateKeyPress(KeyCode.A);
            Debug.Log("a");
        }
        if (isPressingS)
        {
            movimientoScript.SimulateKeyPress(KeyCode.S);
            Debug.Log("s");
        }
        if (isPressingD)
        {
            movimientoScript.SimulateKeyPress(KeyCode.D);
            Debug.Log("d");
        }
    }
}
