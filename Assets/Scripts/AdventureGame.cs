using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdventureGame : MonoBehaviour
{
    // to make the script static and able to call within other scripts with the Gameobject
    public static AdventureGame ad;

    //Referencing the GameObjects within the Game
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

    //Input Value for Mana and Health
    [Header("Health and Mana Setting")]
    public int maxHealth;
    public int maxMana;
    int health;
    int mana;
    int gold;

    //Initilizing the variable for storing various states within the game
    State state;

    private void Awake()
    {
        //If the script is not initialized then initilize the script
        if (ad == null)
        {
            ad = this.GetComponent<AdventureGame>();
        }
    }

   

    // Start is called before the first frame update
    void Start()
    {
        InitializeHealth(); //Initialize the Health at the begining
        InitializeMana();   //Initialize the Mana at the begining
        InitializeGold();   //Initialize the Gold at the begining

        state = startingState; //Set the starting state as the initial state in the beginning
        StateManager(state);    // Manange the state
    }

    // Update is called once per frame
    void Update()
    {   
        //If the exit button is pressed, exit the application
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void whenOptionButtonClicked(int buttonIndex)
    {
        //If the health is zero then gameOver
        if (health <= 0)
        {
            state = gameOverState;
            StateManager(state);
            InitializeHealth();
            InitializeGold();
            InitializeMana();
        }
        else
        {
            var nextStates = state.GetNextStates();
            state = nextStates[buttonIndex];
            StateHealthManaGoldManager(state);
            StateManager(state); 
        }
        
    }


    private void InstantiateOptionButton(string[] options, string[] manaDependentBtn, int[] requiredMana)
    {
        //If the choiceparentsection already has options then destory the options
        if (choiceParentSection.transform.childCount != 0)
        {
            foreach(Transform child in choiceParentSection.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        float x_pos = 0;    // Initial x-pos of the option
        float y_pos = 0;    //Initial y-pos of the option
        //Generate the options and set the text for the state
        for (int optionIndex = 0; optionIndex < options.Length; optionIndex++)
        {
            OptionButton optionButton = Instantiate(Prefab);
            optionButton.transform.SetParent(choiceParentSection.transform);
            optionButton.transform.localPosition = new Vector3(x_pos, y_pos, 0);
            optionButton.setOptionText(options[optionIndex]);
            y_pos = (float)(y_pos - 76.8);
            for(int manaDependentBtnIndex=0; manaDependentBtnIndex< manaDependentBtn.Length; manaDependentBtnIndex++)
            {
                var btn = manaDependentBtn[manaDependentBtnIndex];
                int lastIndex = (int)Char.GetNumericValue(btn[btn.Length - 1]);
                if (lastIndex == optionIndex)
                {
                    if (mana < requiredMana[lastIndex])
                    {
                        optionButton.disableButton();
                    }
                }
            }
        }
    }

    // Set the gold collected text in the Game UI 
    private void SetGoldCollectedText(int gold)
    {
        goldCollectedComponent.text = gold.ToString();
    }
    // Initialize the Health of the Player
    private void InitializeHealth()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(health);
        healthBar.SetHealth(health);
    }
    // Initialize the Health of the Player
    private void InitializeMana()
    {
        mana = maxMana;
        manaBar.setMaxMana(mana);
        manaBar.setMana(mana);
    }
    //Initialize the gold text
    private void InitializeGold()
    {
        gold = 0;
        SetGoldCollectedText(gold);
    }

    // Manange the Story Text, Generate the Options and Manage the Image of the given state
    private void StateManager(State state)
    {
        textComponent.text = state.GetStateStory();
        questionComponent.text = state.GetStateQuestion();
        var options = state.GetStateOption();
        var manaDependentBtn = state.GetManaDependentButton();
        var requiredMana = state.GetRequiredMana();
        InstantiateOptionButton(options,manaDependentBtn,requiredMana);
        if (state.GetChangeStatus())
        {
            storyImage.sprite = state.GetStoryImage();
        }
    }

    private void StateHealthManaGoldManager(State state)
    {
        //Refill the health and Mana to maximum during rest or while using potion
        if (-state.GetHealthDamage() > maxHealth && -state.GetManaDamage() > maxMana)
        {
            health = maxHealth;
            mana = maxMana;
        }
        else
        {
            //Health and Mana damage while fighting
            health -= state.GetHealthDamage();
            mana -= state.GetManaDamage();

        }
        //Setting the Health, gold and Mana
        gold += state.GetGoldCollected();
        healthBar.SetHealth(health);
        manaBar.setMana(mana);
        SetGoldCollectedText(gold);
    }
}
