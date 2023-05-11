using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float followRange = 35f; // Maximum distance for following
    
    void Update()
    {
        // Calculate the direction to the player
        Vector3 direction = player.position - transform.position;
        float distanceToPlayer = direction.magnitude;
        
        // Check if the player is within range
        if (distanceToPlayer <= followRange)
        {
            direction.Normalize();
            
            // Turn to face the player
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            
            // Move towards the player
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
