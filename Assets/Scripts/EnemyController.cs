using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform car; 
    public float speed = 12f; 
    public float detectionRange = 20f; 
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 20;
    private Animator animator; 
    private Collider triggerCollider;
    [SerializeField] private GameObject destroyParticlePrefab; // Reference to the particle system prefab

    void Start()
    {
        car = CarController.Instance.transform;
        animator = GetComponent<Animator>(); 
        triggerCollider = GetComponentInChildren<Collider>(); 
        if (triggerCollider != null)
        {
            triggerCollider.isTrigger = true;
        }
        else
        {
            Debug.LogError("No trigger collider found on the enemy!");
        }
    }

    void Update()
    {
        float distanceToCar = Vector3.Distance(transform.position, car.position);

        if (distanceToCar < detectionRange)
        {
            Vector3 direction = (car.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);

            transform.position = Vector3.MoveTowards(transform.position, car.position, speed * Time.deltaTime);
            animator.SetTrigger("start_run"); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            CarController.Instance.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        // Instantiate and play the particle system
        if (destroyParticlePrefab != null)
        {
            GameObject particleInstance = Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity);
            ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play();
                Destroy(particleInstance, particleSystem.main.duration); // Destroy the particle system after it finishes
            }
        }
        Destroy(gameObject);
    }
}