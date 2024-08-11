using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    private Controles controles;

    private void Awake()
    {
        controles = new Controles();
        controles.General.Navegar.performed += ctx => OnNavigate(ctx.ReadValue<Vector2>());
        controles.General.Avanzar.performed += ctx => OnSubmit();
    }

    private void OnEnable()
    {
        controles.Enable();
    }

    private void OnDisable()
    {
        controles.Disable();
    }

    private void OnNavigate(Vector2 navigationInput)
    {
        if (EventSystem.current == null)
        {
            Debug.LogWarning("No EventSystem found in the scene. Please add an EventSystem.");
            return;
        }

        // Get the currently selected GameObject
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

        if (selectedObject == null)
        {
            Debug.LogWarning("No GameObject is currently selected.");
            return;
        }

        // Create an AxisEventData to simulate navigation
        AxisEventData axisEventData = new AxisEventData(EventSystem.current)
        {
            moveVector = navigationInput,
            moveDir = DetermineMoveDirection(navigationInput.x, navigationInput.y)
        };

        // Send the navigation event to the currently selected GameObject
        ExecuteEvents.Execute(selectedObject, axisEventData, ExecuteEvents.moveHandler);
    }

    private void OnSubmit()
    {
        if (EventSystem.current == null)
        {
            Debug.LogWarning("No EventSystem found in the scene. Please add an EventSystem.");
            return;
        }

        // Get the currently selected GameObject
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

        if (selectedObject == null)
        {
            Debug.LogWarning("No GameObject is currently selected.");
            return;
        }

        // Send the submit event to the currently selected GameObject
        ExecuteEvents.Execute(selectedObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
    }

    private MoveDirection DetermineMoveDirection(float x, float y)
    {
        if (x > 0) return MoveDirection.Right;
        if (x < 0) return MoveDirection.Left;
        if (y > 0) return MoveDirection.Up;
        if (y < 0) return MoveDirection.Down;
        return MoveDirection.None;
    }
}
