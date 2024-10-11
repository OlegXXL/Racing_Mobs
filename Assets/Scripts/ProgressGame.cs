using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressGame : MonoBehaviour
{
    [SerializeField] private Transform car; // Reference to the car transform
    [SerializeField] private Transform finish; // Reference to the finish transform
    [SerializeField] private Slider progressSlider; // Reference to the UI Slider

    private float initialDistance;

    // Start is called before the first frame update
    void Start()
    {
        if (car != null && finish != null)
        {
            // Calculate the initial distance between the car and the finish line
            initialDistance = Vector3.Distance(car.position, finish.position);
            // Set the slider's max value to the initial distance
            progressSlider.maxValue = initialDistance;
            progressSlider.value = 0; // Start the slider at 0
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (car != null && finish != null)
        {
            // Calculate the current distance between the car and the finish line
            float currentDistance = Vector3.Distance(car.position, finish.position);
            // Update the slider's value based on the reversed distance
            progressSlider.value = initialDistance - currentDistance;
        }
    }
}