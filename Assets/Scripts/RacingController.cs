using UnityEngine;

public class RacingController : MonoBehaviour
{
    public static RacingController Instance { get; private set; }
    [SerializeField] float speed = 10f;
    private bool isStopped = true;

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

    void Update()
    {
        if (!isStopped)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }

    public void StartMovement()
    {
        isStopped = false;
    }

    public void StopMovement()
    {
        isStopped = true;
    }

    public void ResetPosition()
    {
        transform.position = Vector3.zero;
        isStopped = true;
    }
}