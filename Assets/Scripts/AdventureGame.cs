using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdventureGame : MonoBehaviour
{

    public static AdventureGame ad;

    [Header("Reference Settings")]
    [SerializeField] OptionButton Prefab;
    [SerializeField] State startingState;
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] TextMeshProUGUI questionComponent;
    [SerializeField] GameObject choiceParentSection;
    [SerializeField] Image storyImage;
    [SerializeField] HealthBar healthBar;
    [SerializeField] ManaBar manaBar;
    [SerializeField] TextMeshProUGUI goldCollectedComponent;
    [SerializeField] State gameOverState;

    [Header("Health and Mana Setting")]
    public int maxHealth;
    public int maxMana;
    int health;
    int mana;
    int gold;


    State state;

    private void Awake()
    {
        if (ad == null)
        {
            ad = this.GetComponent<AdventureGame>();
        }
    }

    private void SetGoldCollectedText(int gold)
    {
        goldCollectedComponent.text = gold.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        mana = maxMana;
        healthBar.SetMaxHealth(health);
        manaBar.setMaxMana(mana);
        healthBar.SetHealth(health);
        manaBar.setMana(mana);
        gold = 0;
        SetGoldCollectedText(gold);

        state = startingState;
        textComponent.text = state.GetStateStory();
        questionComponent.text = state.GetStateQuestion();
        var options = state.GetStateOption();
        InstantiateOptionButton(options);
        storyImage.sprite = state.GetStoryImage();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void whenOptionButtonClicked(int buttonIndex)
    {
        if (health <= 0)
        {
            state = gameOverState;
            textComponent.text = state.GetStateStory();
            questionComponent.text = state.GetStateQuestion();
            var options = state.GetStateOption();
            InstantiateOptionButton(options);
            if (state.GetChangeStatus())
            {
                Debug.Log(state.GetStoryImage());
                storyImage.sprite = state.GetStoryImage();
            }
            health = maxHealth;
            mana = maxMana;
            gold = 0;
        }
        else
        {
            var nextStates = state.GetNextStates();
            state = nextStates[buttonIndex];
            textComponent.text = state.GetStateStory();
            questionComponent.text = state.GetStateQuestion();
            var options = state.GetStateOption();
            InstantiateOptionButton(options);
            if (state.GetChangeStatus())
            {
                Debug.Log(state.GetStoryImage());
                storyImage.sprite = state.GetStoryImage();
            }
            if(-state.GetHealthDamage()>maxHealth && -state.GetManaDamage() > maxMana)
            {
                health = maxHealth;
                mana = maxMana;
            }
            else
            {
                health -= state.GetHealthDamage();
                mana -= state.GetManaDamage();
                
            }
            gold += state.GetGoldCollected();
            healthBar.SetHealth(health);
            manaBar.setMana(mana);
            SetGoldCollectedText(gold);
        }
        
    }


    private void InstantiateOptionButton(string[] options)
    {
        if (choiceParentSection.transform.childCount != 0)
        {
            foreach(Transform child in choiceParentSection.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        float x_pos = 0;
        float y_pos = 0;
        for (int optionIndex = 0; optionIndex < options.Length; optionIndex++)
        {
            OptionButton optionButton = Instantiate(Prefab);
            optionButton.transform.SetParent(choiceParentSection.transform);
            optionButton.transform.localPosition = new Vector3(x_pos, y_pos, 0);
            optionButton.setOptionText(options[optionIndex]);
            y_pos = (float)(y_pos - 76.8);
        }
    }
}
