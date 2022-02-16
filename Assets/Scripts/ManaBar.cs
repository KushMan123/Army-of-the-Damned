using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI currentManaTextComponent;
    public TextMeshProUGUI maxManaTextComponent;

    public void setMana(int mana)
    {
        slider.value = mana;
        currentManaTextComponent.text = mana.ToString();
    }

    public void setMaxMana(int maxMana)
    {
        slider.maxValue = maxMana;
        maxManaTextComponent.text = maxMana.ToString();
    }
}
