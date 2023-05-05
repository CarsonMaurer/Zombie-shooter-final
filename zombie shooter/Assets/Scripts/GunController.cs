using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform gunBarrel;
    public float bulletSpeed = 20f;
    public float cooldownTime = 0.5f; 
    public float bulletLifetime = 3f; 
    public AnimationClip shootingAnimationClip;

    private Transform playerTransform;
    private float lastShotTime = Mathf.NegativeInfinity;

    private void Start()
    {
        playerTransform = transform.parent; 
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

   
    Vector3 spawnPosition = gunBarrel.position + gunBarrel.forward * 0.5f;

    GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
    Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
    bulletRigidbody.velocity = playerTransform.forward * bulletSpeed;
    bulletRigidbody.useGravity = false;

    
    bullet.transform.rotation = gunBarrel.rotation * Quaternion.Euler(90f, 0f, 0f);

    Destroy(bullet, bulletLifetime);

   
    for (int i = -1; i <= 1; i += 2)
    {
        Quaternion rotation = Quaternion.Euler(0f, i * 25f, 0f);
        Quaternion zRotation = Quaternion.Euler(0f, 0f, i * -120f);
        GameObject bulletClone = Instantiate(bulletPrefab, spawnPosition, rotation * zRotation);
        Rigidbody bulletCloneRigidbody = bulletClone.GetComponent<Rigidbody>();
        bulletCloneRigidbody.velocity = (rotation * playerTransform.forward) * bulletSpeed;
        bulletCloneRigidbody.useGravity = false;

       
        bulletClone.transform.rotation = gunBarrel.rotation * Quaternion.Euler(90f, 0f, 0f);
         
    

        
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletClone.GetComponent<Collider>());

        Destroy(bulletClone, bulletLifetime);
    }
   
}










}
