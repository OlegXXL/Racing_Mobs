using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSlider : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    void Start()
    {
        if (CarController.Instance != null)
        {
            healthSlider.maxValue = CarController.Instance.health;
            healthSlider.value = CarController.Instance.health;

            CarController.Instance.OnHealthChanged += UpdateHealthSlider;
        }
    }

    void OnDestroy()
    {
        if (CarController.Instance != null)
        {
            CarController.Instance.OnHealthChanged -= UpdateHealthSlider;
        }
    }

    private void UpdateHealthSlider(int currentHealth)
    {
        healthSlider.value = currentHealth;
    }
}