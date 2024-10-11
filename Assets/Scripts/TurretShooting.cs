using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public static TurretShooting Instance { get; private set; }
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform turret; // Reference to the turret transform
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    private bool isStopped = false; // Flag to check if the shooting is stopped

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!isStopped && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }
    public void StartShooting()
    {
    isStopped = false;
    }
    private void Shoot()
    {
        // Instantiate the bullet at the bulletSpawnPoint's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        
        // Adjust the bullet's rotation to ensure it has 0 degrees on the X-axis
        bullet.transform.rotation = Quaternion.Euler(0, bulletSpawnPoint.rotation.eulerAngles.y, bulletSpawnPoint.rotation.eulerAngles.z);
    }

    public void StopShooting()
    {
        isStopped = true;
    }
}