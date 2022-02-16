using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComponent;

    public string getOptionText()
    {
        return textComponent.text;
    }

    public void setOptionText(string newOptionText)
    {
        textComponent.text = newOptionText;
    }
}
