using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // The speed at which the player moves
    public float rotationSpeed = 200f; // The speed at which the player rotates
    private Rigidbody rb; // The Rigidbody component attached to the player

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Freeze rotation of the Rigidbody
    }

    void FixedUpdate()
    {
        // Get the horizontal and vertical inputs
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate the movement direction
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Rotate the player to face the movement direction
        if (movementDirection.magnitude != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }

        // Move the player in the movement direction
        rb.velocity = movementDirection * moveSpeed;

        // If the player is not moving, stop the player's movement
        if (movementDirection.magnitude == 0)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
