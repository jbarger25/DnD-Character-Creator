using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{

    public GameObject MainCanvas;
    public GameObject Panel_Main, Panel_Char1;
    public InputField Name_Input, JSON_Output_IF;
    public Dropdown AbilityDropDown, RaceDropDown, AlignmentDropDowm, ClassDropDown;
    public DiceRoller rollDice;
    public PlayerController playerState;
    public Text STR_Value, DEX_Value, CON_Value, INT_Value, WIS_Value, CHA_Value, SPD_Value, AC_Value, HP_Value, XP_Value;
    public Text Items_Text;

    Dictionary<string, int> abilities = new Dictionary<string, int>();
    public void Awake()
    {
        Panel_Main.gameObject.SetActive(true);
        Panel_Char1.gameObject.SetActive(false);
    }
    void Start()
    {
        rollDice = DiceRoller.instance;
        playerState = PlayerController.instance;
        MainCanvas = GameObject.Find("Canvas");
        Panel_Main = MainCanvas.transform.Find("Panel_Main").gameObject;
        Panel_Char1 = MainCanvas.transform.Find("Panel_Char1").gameObject;
        AbilityDropDown = Panel_Char1.transform.Find("Ability_Dropdown").GetComponent<Dropdown>();
        RaceDropDown = Panel_Char1.transform.Find("Race_DropDown").GetComponent<Dropdown>();
        AlignmentDropDowm = Panel_Char1.transform.Find("Alignment_DropDown").GetComponent<Dropdown>();
        ClassDropDown = Panel_Char1.transform.Find("Class_DropDown").GetComponent<Dropdown>();
        STR_Value = Panel_Char1.transform.Find("Strength").GetComponentInChildren<Text>(true);
        DEX_Value = Panel_Char1.transform.Find("Dexterity").GetComponentInChildren<Text>(true);
        CON_Value = Panel_Char1.transform.Find("Constitution").GetComponentInChildren<Text>(true);
        INT_Value = Panel_Char1.transform.Find("Intelligence").GetComponentInChildren<Text>(true);
        WIS_Value = Panel_Char1.transform.Find("Wisdom").GetComponentInChildren<Text>(true);
        CHA_Value = Panel_Char1.transform.Find("Charisma").GetComponentInChildren<Text>(true);
        Items_Text = Panel_Char1.transform.Find("Items").GetComponentInChildren<Text>(true);
        Name_Input = Panel_Char1.transform.Find("Name").GetComponentInChildren<InputField>(true);
        JSON_Output_IF = Panel_Char1.transform.Find("JSON").GetComponentInChildren<InputField>(true);

    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = (false);
#else
        Application.Quit();
#endif
    }

    public void PlayTest()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
        Panel_Main.gameObject.SetActive(true);
        Panel_Char1.gameObject.SetActive(false);
    }

    public void RollCharacter()
    {
        //SceneManager.LoadScene("CharacterGenerator");
        Panel_Main.gameObject.SetActive(false);
        Panel_Char1.gameObject.SetActive(true);
    }
    public void SaveAbility()
    {
        int rollTotal = rollDice.GetRollTotal();
        string ability = AbilityDropDown.options[AbilityDropDown.value].text;
        if (abilities.ContainsKey(ability))
        {
            abilities[ability] = rollTotal;
        }
        else
        {
            abilities.Add(ability, rollTotal);
        }
        if (ability.Equals("Dexterity"))
        {
            playerState.AC = 12;
            AC_Value.text = playerState.AC.ToString();
        }
        if (abilities.Count == 6)
        {
            playerState.MakeJSON();
        }
        OutputAbilityToBox(ability, rollTotal);
    }
    public void OutputAbilityToBox(string abilityName, int rollTotal)
    {
        if (abilityName.Equals("Strength"))
        {
            STR_Value.text = rollTotal.ToString();
            playerState.Ability_Strength = rollTotal;
        }
        else if (abilityName.Equals("Dexterity"))
        {
            DEX_Value.text = rollTotal.ToString();
            playerState.Ability_Dexterity = rollTotal;
        }
        else if (abilityName.Equals("Constitution"))
        {
            CON_Value.text = rollTotal.ToString();
            playerState.Ability_Constitution = rollTotal;
        }
        else if (abilityName.Equals("Intelligence"))
        {
            INT_Value.text = rollTotal.ToString();
            playerState.Ability_Intelligence = rollTotal;
        }
        else if (abilityName.Equals("Wisdom"))
        {
            WIS_Value.text = rollTotal.ToString();
            playerState.Ability_Wisdom = rollTotal;
        }
        else if (abilityName.Equals("Charisma"))
        {
            CHA_Value.text = rollTotal.ToString();
            playerState.Ability_Charisma = rollTotal;
        }
    }
    public void SelectAlignment()
    {
        playerState.Alignment = AlignmentDropDowm.options[AlignmentDropDowm.value].text;
        Debug.Log(playerState.Alignment);
    }
    public void SelectRace()
    {
        string race = RaceDropDown.options[RaceDropDown.value].text;
        playerState.Race = race;
        if (race.Equals("Dragonborn") || race.Equals("Elf") || race.Equals("Half-Elf") || race.Equals("Half-Orc") || race.Equals("Human") || race.Equals("Tiefling"))
        {
            playerState.Running_Speed = 30;
            playerState.Walking_Speed = playerState.Running_Speed / 2;
        }
        else
        {
            playerState.Running_Speed = 25;
            playerState.Walking_Speed = playerState.Running_Speed / 2;
        }
        SPD_Value.text = playerState.Running_Speed.ToString();
        Debug.Log(playerState.Race);
    }
    public void SelectClass()
    {
        int hp;

        string Class = ClassDropDown.options[ClassDropDown.value].text;
        playerState.SetClass(Class);
        if (Class.Equals("Barbarian"))
        {
            hp = 12 + 2;
            playerState.SetMaxHP(hp);
            playerState.SetCurrentHP(hp);
        }
        else if (Class.Equals("Bard") || Class.Equals("Cleric") || Class.Equals("Druid") || Class.Equals("Monk") || Class.Equals("Rogue") || Class.Equals("Warlock"))
        {
            hp = 8 + 2;
            playerState.SetMaxHP(hp);
            playerState.SetCurrentHP(hp);
        }
        else if (Class.Equals("Fighter") || Class.Equals("Paladin") || Class.Equals("Ranger"))
        {
            hp = 10 + 2;
            playerState.SetMaxHP(hp);
            playerState.SetCurrentHP(hp);
        }
        else if (Class.Equals("Sorcerer") || Class.Equals("Wizard"))
        {
            hp = 6 + 2;
            playerState.SetMaxHP(hp);
            playerState.SetCurrentHP(hp);
        }
        Debug.Log(Class);
    }

    public void SelectName()
    {
        string Name_Text = Name_Input.text;
        playerState.CharacterName = Name_Text;
        Debug.Log(playerState.CharacterName);
    }

    public void DisplayItems()
    {
        string items = "Items: ";
        foreach(string s in playerState.Items)
        {
            items = items + s + ", ";
        }
        Items_Text.text = items;
    }
}
