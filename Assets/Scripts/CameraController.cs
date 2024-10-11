using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    public Camera startCamera;
    public Camera gameCamera;
    public Camera loseCamera;

    public GameObject car;

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

    void Start()
    {
        SwitchToStartCamera();
    }

    public void SwitchToStartCamera()
    {
        startCamera.enabled = true;
        gameCamera.enabled = false;
        loseCamera.enabled = false;
    }

    public void SwitchToGameCamera()
    {
        startCamera.enabled = false;
        gameCamera.enabled = true;
        loseCamera.enabled = false;
    }

    public void SwitchToLoseCamera()
    {
        startCamera.enabled = false;
        gameCamera.enabled = false;
        loseCamera.enabled = true;

        Vector3 offset = new Vector3(-10, 10, -10);
        loseCamera.transform.position = car.transform.position + offset;
        loseCamera.transform.LookAt(car.transform.position + new Vector3(0, 2, 0));
    }

    public IEnumerator SmoothTransitionToGameCamera(float duration)
    {
        Vector3 startPosition = startCamera.transform.position;
        Quaternion startRotation = startCamera.transform.rotation;
        Vector3 endPosition = gameCamera.transform.position;
        Quaternion endRotation = gameCamera.transform.rotation;

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            startCamera.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            startCamera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        startCamera.transform.position = endPosition;
        startCamera.transform.rotation = endRotation;

        SwitchToGameCamera();
    }

    public IEnumerator SmoothTransitionToLoseCamera(float duration)
    {
        loseCamera.enabled = true; // Enable the lose camera during the transition

        Vector3 startPosition = gameCamera.transform.position;
        Quaternion startRotation = gameCamera.transform.rotation;
        Vector3 offset = new Vector3(-10, 10, -10);

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            Vector3 targetPosition = car.transform.position + offset;
            loseCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            loseCamera.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.LookRotation(car.transform.position + new Vector3(0, 2, 0) - loseCamera.transform.position), elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        loseCamera.transform.SetParent(car.transform);
        SwitchToLoseCamera(); 
    }
}