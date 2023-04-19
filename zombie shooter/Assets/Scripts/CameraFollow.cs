using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // The player object to follow
    public Vector3 offset; // The offset of the camera from the player

    void LateUpdate()
    {
        transform.position = player.position + offset; // Set the position of the camera to follow the player with the offset

        transform.rotation = Quaternion.Euler(90f, 0f, 0f); // Set the camera rotation to look down at the player
    }
}
