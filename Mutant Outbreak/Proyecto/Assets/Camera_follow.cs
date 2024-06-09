using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; 

    void LateUpdate()
    {
        
        if (player != null)
        {
            
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            
            
            transform.position = targetPosition;
        }
    }
}
