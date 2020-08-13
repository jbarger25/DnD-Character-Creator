using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public DiceRoller rollDice;
    public UI_Controller uiController;
    public PlayerController playerState;
    public Text JSON_Output;
    public GameObject Canvas;
    public void Awake()
    {
        rollDice = DiceRoller.instance;
        playerState = PlayerController.instance;
        uiController = GameObject.FindObjectOfType(typeof(UI_Controller)) as UI_Controller;
    }
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
        JSON_Output = Canvas.transform.Find("JSON_OUT").GetComponentInChildren<Text>(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        SceneManager.LoadScene("CharacterGenerator");
    }

    public void ToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Test()
    {
        Debug.Log(playerState.JSON_String);
    }
}
