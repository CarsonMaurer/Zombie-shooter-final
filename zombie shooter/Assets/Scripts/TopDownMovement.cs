using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // The speed at which the player moves
    public float rotationSpeed = 200f; // The speed at which the player rotates
    private Rigidbody rb; // The Rigidbody component attached to the player
    private bool isReversing; // Flag to check if the player is reversing

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

        // Check if the player is reversing
        if (verticalInput < 0)
        {
            isReversing = true;
        }
        else
        {
            isReversing = false;
        }

        // Calculate the movement direction
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, Mathf.Abs(verticalInput)).normalized;

        // Rotate the player to face the movement direction
        if (movementDirection.magnitude != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }

        // Move the player in the movement direction
        if (isReversing)
        {
            rb.velocity = -transform.forward * moveSpeed;
        }
        else
        {
            rb.velocity = transform.forward * moveSpeed;
        }

        // If the player is not moving, stop the player's movement
        if (movementDirection.magnitude == 0)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
