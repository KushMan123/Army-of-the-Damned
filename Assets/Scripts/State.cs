using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="State")]
public class State : ScriptableObject
{
    [Header("Story Text Setting")]
    [TextArea(20,40)][SerializeField] string storyText;

    [Header("Story Question Setting")]
    [TextArea(2, 5)] [SerializeField] string question;

    [Header("Story NextStates Setting")]
    [SerializeField] State[] nextStates;
    

    [Header("Story StateOptions Setting")]
    [TextArea(4, 5)] [SerializeField] string[] options;
    [SerializeField] string[] ManaDependentOption;
    [SerializeField] string[] FightInitiationOption;
    [SerializeField] int[] requiredMana;

    [Header("Story Image Settings")]
    [SerializeField] bool changeImage;
    [SerializeField] Sprite storyImage;

    [Header("Health and Mana and Gold Settings")]
    [SerializeField] int healthDamage;
    [SerializeField] int manaDamage;
    [SerializeField] int goldCollected;

    public string GetStateStory()
    {
        return storyText;
    }

    public State[] GetNextStates()
    {
        return nextStates;
    }

    public string GetStateQuestion()
    {
        return question;
    }

    public string[] GetStateOption()
    {
        return options;
    }

    public bool GetChangeStatus()
    {
        return changeImage;
    }

    public Sprite GetStoryImage()
    {
        return storyImage;
    }

    public int GetHealthDamage()
    {
        return healthDamage;
    }

    public int GetManaDamage()
    {
        return manaDamage;
    }

    public int GetGoldCollected()
    {
        return goldCollected;
    }

    public string[] GetManaDependentButton()
    {
        return ManaDependentOption;
    }

    public int[] GetRequiredMana()
    {
        return requiredMana;
    }
}
