using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] Button buttonComponent;

    public void onChoiceButtonClick()
    {
        AdventureGame.ad.whenOptionButtonClicked(gameObject.transform.GetSiblingIndex());
    }
    
    public void disableButton()
    {
        buttonComponent.interactable = false;
    }

    public void enableButton()
    {
        buttonComponent.interactable = true;
    }

    public string getOptionText()
    {
        return textComponent.text;
    }

    public void setOptionText(string newOptionText)
    {
        textComponent.text = newOptionText;
    }
}
