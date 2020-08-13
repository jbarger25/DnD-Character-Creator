using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    public static DiceRoller instance = null;

    public UI_Controller uiController;
    public Text Box_1_Text, Box_2_Text, Box_3_Text, Box_4_Text, Box_5_Text, Box_Total_Text;
    private int rollTotal;
    private int diceRolled;
    List<int> rolls = new List<int>();

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
                Destroy(this);
            }
        }
    }
    void Start()
    {
        uiController = GameObject.FindObjectOfType(typeof(UI_Controller)) as UI_Controller;
        Box_1_Text = uiController.Panel_Char1.transform.Find("Box_1").GetComponentInChildren<Text>(true);
        Box_2_Text = uiController.Panel_Char1.transform.Find("Box_2").GetComponentInChildren<Text>(true);
        Box_3_Text = uiController.Panel_Char1.transform.Find("Box_3").GetComponentInChildren<Text>(true);
        Box_4_Text = uiController.Panel_Char1.transform.Find("Box_4").GetComponentInChildren<Text>(true);
        Box_5_Text = uiController.Panel_Char1.transform.Find("Box_5").GetComponentInChildren<Text>(true);
        Box_Total_Text = uiController.Panel_Char1.transform.Find("Box_Total").GetComponentInChildren<Text>(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DiceRoll()
    {
        diceRolled++;
        if(diceRolled == 6)
        {
            uiController.SaveAbility();
            ClearRolls();
            return;
        }
        rolls.Add(Random.Range(1, 7));
        if (diceRolled == 1)
            Box_1_Text.text = rolls.ElementAt(0).ToString();
        else if (diceRolled == 2)
            Box_2_Text.text = rolls.ElementAt(1).ToString();
        else if (diceRolled == 3)
            Box_3_Text.text = rolls.ElementAt(2).ToString();
        else if (diceRolled == 4)
            Box_4_Text.text = rolls.ElementAt(3).ToString();
        else if (diceRolled == 5)
        {
            Box_5_Text.text = rolls.ElementAt(4).ToString();
            rolls.Sort();
            rolls.RemoveRange(0, 2);
            rollTotal = rolls.Sum();
            Box_Total_Text.text = rollTotal.ToString();
        }
    }

    public void ClearRolls()
    {
        Box_1_Text.text = "--";
        Box_2_Text.text = "--";
        Box_3_Text.text = "--";
        Box_4_Text.text = "--";
        Box_5_Text.text = "--";
        Box_Total_Text.text = "--";
        diceRolled = 0;
        rollTotal = 0;
        rolls.Clear();
    }
    public int GetRollTotal()
    {
        Debug.Log(rollTotal.ToString());
        return this.rollTotal;
    }
    
}
