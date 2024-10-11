using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControll : MonoBehaviour
{
    [SerializeField] private GameObject turret;
    public float rotationSpeed = 100.0f;
    public float returnSpeed = 200.0f;

    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private bool isTouching = false;
    private Quaternion initialRotation;
    private float currentRotationY = 0.0f;

    public bool isGameOver = false;

    private void Start()
    {
        initialRotation = turret.transform.rotation;
    }

    private void Update()
    {
        if (isGameOver)
        {
            return; 
        }

        HandleTouchInput();

        if (!isTouching)
        {
            ReturnToInitialPosition();
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                isTouching = true;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                touchEndPos = touch.position;
                RotateTurret();
                touchStartPos = touchEndPos;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouching = false;
            }
        }
    }

    private void RotateTurret()
    {
        float screenWidth = Screen.width;
        float deltaX = touchEndPos.x - touchStartPos.x;
        float rotationAmount = (deltaX / screenWidth) * 180.0f;

        currentRotationY -= rotationAmount;

        turret.transform.rotation = Quaternion.Euler(-90, currentRotationY, 180);
    }

    private void ReturnToInitialPosition()
    {
        currentRotationY = Mathf.Lerp(currentRotationY, 0, returnSpeed * Time.deltaTime);
        turret.transform.rotation = Quaternion.Euler(-90, currentRotationY, 180);
    }
}