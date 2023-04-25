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

        GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = playerTransform.forward * bulletSpeed;
        bulletRigidbody.useGravity = false;

        // Rotate the bullet to match the gun barrel's rotation
        bullet.transform.rotation = gunBarrel.rotation * Quaternion.Euler(90f, 0f, 0f);

        Destroy(bullet, bulletLifetime);
    }
}
