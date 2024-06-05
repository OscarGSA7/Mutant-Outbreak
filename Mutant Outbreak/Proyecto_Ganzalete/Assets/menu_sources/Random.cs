using UnityEngine;
using UnityEngine.UI;

public class MoverMono : MonoBehaviour
{
    public RectTransform Mono; 
    public Text xPosLabel;     
    public Text yPosLabel;     

    void Start()
    {
        if (Mono == null || xPosLabel == null || yPosLabel == null)
        {
            Debug.LogError("Referencias no asignadas en el inspector.");
            return;
        }
        MoveToRandomPosition();
    }

    
    void MoveToRandomPosition()
    {
        
        RectTransform parentRectTransform = Mono.parent as RectTransform;

        
        float randomX = Random.Range(-parentRectTransform.rect.width / 2, parentRectTransform.rect.width / 2);
        float randomY = Random.Range(-parentRectTransform.rect.height / 2, parentRectTransform.rect.height / 2);

        
        Vector2 newPosition = new Vector2(randomX, randomY);

        
        Mono.anchoredPosition = newPosition;

        
        xPosLabel.text = "X: " + randomX.ToString("F2");
        yPosLabel.text = "Y: " + randomY.ToString("F2");

        Debug.Log($"Mono movido a: {newPosition}");
    }

    
    public void MoveToNewRandomPosition()
    {
        MoveToRandomPosition();
    }
}
