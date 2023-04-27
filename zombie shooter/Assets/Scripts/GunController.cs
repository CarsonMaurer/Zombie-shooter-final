using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform gunBarrel;
    public float bulletSpeed = 20f;
    public float cooldownTime = 0.5f; // Time in seconds between shots
    public float bulletLifetime = 3f; // Time in seconds before a bullet is destroyed

    private Transform playerTransform;
    private float lastShotTime = Mathf.NegativeInfinity;

    private void Start()
    {
        playerTransform = transform.parent; // assuming the gun is a child of the player object
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > lastShotTime + cooldownTime)
        {
            Shoot();
        }
    }

private void Shoot()
{
    lastShotTime = Time.time;

    // Spawn the initial bullet
    Vector3 spawnPosition = gunBarrel.position + gunBarrel.forward * 0.5f;

    GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
    Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
    bulletRigidbody.velocity = playerTransform.forward * bulletSpeed;
    bulletRigidbody.useGravity = false;

    // Rotate the bullet to match the gun barrel's rotation
    bullet.transform.rotation = gunBarrel.rotation * Quaternion.Euler(90f, 0f, 0f);

    Destroy(bullet, bulletLifetime);

    // Spawn two additional bullets with a rotation of +/- 35 degrees around the y-axis and a z-rotation of -120 and 120
    for (int i = -1; i <= 1; i += 2)
    {
        Quaternion rotation = Quaternion.Euler(0f, i * 25f, 0f);
        Quaternion zRotation = Quaternion.Euler(0f, 0f, i * -120f);
        GameObject bulletClone = Instantiate(bulletPrefab, spawnPosition, rotation * zRotation);
        Rigidbody bulletCloneRigidbody = bulletClone.GetComponent<Rigidbody>();
        bulletCloneRigidbody.velocity = (rotation * playerTransform.forward) * bulletSpeed;
        bulletCloneRigidbody.useGravity = false;

        // Rotate the bullet to match the gun barrel's rotation
        bulletClone.transform.rotation = gunBarrel.rotation * Quaternion.Euler(90f, 0f, 0f);

        // Disable collisions between the bullets
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletClone.GetComponent<Collider>());

        Destroy(bulletClone, bulletLifetime);
    }
} 










}
