using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void onChoiceButtonClick()
    {
        AdventureGame.ad.whenOptionButtonClicked(gameObject.transform.GetSiblingIndex());
    }
}
