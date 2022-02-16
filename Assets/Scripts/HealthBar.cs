using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI currentHealthTextComponent;
    public TextMeshProUGUI maxHealthTextComponent;

    public void SetHealth(int health)
    {
        slider.value = health;
        currentHealthTextComponent.text = health.ToString();
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        maxHealthTextComponent.text = maxHealth.ToString();
    }
}
