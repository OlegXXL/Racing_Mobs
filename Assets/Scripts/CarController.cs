using UnityEngine;
using DG.Tweening;

public class CarController : MonoBehaviour
{
    public static CarController Instance { get; private set; }
    public int health = 100;

    public delegate void HealthChanged(int currentHealth);
    public event HealthChanged OnHealthChanged;

    private bool isDestroyed = false;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            EndGame();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        OnHealthChanged?.Invoke(health);

        if (health <= 0 && !isDestroyed)
        {
            DestroyCar();
        }
    }

    void DestroyCar()
    {
        isDestroyed = true;
        Debug.Log("Car destroyed!");
        transform.DORotate(new Vector3(0, 0, 90), 1f, RotateMode.FastBeyond360).SetEase(Ease.OutBounce);
        GameController.Instance.GameOver();
    }

    void EndGame()
    {
        Debug.Log("Game Finished!");
        GameController.Instance.GameFinished(); 
    }

    public void ResetHealth()
    {
        health = 100;
        isDestroyed = false;
    }
}