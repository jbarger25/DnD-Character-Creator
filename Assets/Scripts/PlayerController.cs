using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [System.NonSerialized]
    public DiceRoller rollDice;
    [System.NonSerialized]
    public UI_Controller uiController;
    [System.NonSerialized]
    public string JSON_String;
    [System.NonSerialized]
    public static PlayerController instance = null;

    public string CharacterName;
    public int Ability_Strength, Ability_Dexterity, Ability_Constitution, Ability_Intelligence, Ability_Wisdom, Ability_Charisma;
    public string Race, Class, Alignment;
    public int XpCurrent, XpMax, HpCurrent, HpMax, AC, Walking_Speed, Running_Speed;
    public List<string> Items = new List<string>();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else 
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    void Start()
    {
        rollDice = DiceRoller.instance;
        uiController = GameObject.FindObjectOfType(typeof(UI_Controller)) as UI_Controller;
        uiController.SelectRace();
        uiController.SelectAlignment();
        SetCurrentXP(0);
        SetMaxXP(500);
        uiController.SelectClass();
        CharacterName = "No Name";
    }
    void Update()
    {
        
    }

    public void SetCurrentXP(int value)
    {
        XpCurrent = value;
        uiController.XP_Value.text = XpCurrent.ToString() + "/" + XpMax.ToString();
    }
    public void SetMaxXP(int value)
    {
        XpMax = value;
        uiController.XP_Value.text = XpCurrent.ToString() + "/" + XpMax.ToString();
    }
    public void SetCurrentHP(int value)
    {
        HpCurrent = value;
        uiController.HP_Value.text = HpCurrent.ToString() + "/" + HpMax.ToString();
    }
    public void SetMaxHP(int value)
    {
        HpMax = value;
        uiController.HP_Value.text = HpCurrent.ToString() + "/" + HpMax.ToString();
    }
    public void SetClass(string classSet)
    {
        this.Class = classSet;
    }
    public void MakeJSON()
    {
        this.JSON_String = JsonUtility.ToJson(this);
        Debug.Log(JSON_String);
        uiController.JSON_Output_IF.text = JSON_String;
        uiController.DisplayItems();
    }

    public void Test()
    {
        Debug.Log(JSON_String);
    }
}

