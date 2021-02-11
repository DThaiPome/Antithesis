using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Run game state
    public static bool gameRunning;
    // VR PLayer Lose game state
    public static bool gameLose;
    // VR Player Win game state
    public static bool gameWin;
    // Pause game state
    public static bool gamePause;


    // How many items have been collected
    public static int itemCount;
    // How many items the player needs to collect
    public static int maxItems = 4;

    // Is the final ritual active
    public static bool ritualActive;
    // Has the ritual been completed
    public static bool ritualComplete;
   


    // **Currently WitchStatBlock, PlayerStatBlock, and AEnemyBehaviour have code to manipulate the Game Manager**

    // Start is called before the first frame update
    void Start()
    {
        gameRunning = true;
        gameLose = false;
        gameWin = false;
        gamePause = false;

        itemCount = 0;

        ritualActive = false;
        ritualComplete = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (itemCount == maxItems)
        {
            SetRitualActive();
        }

        if (ritualComplete)
        {
            SetWinState();
        }

    }

    public static bool isGameRunning()
    {
        return gameRunning;
    }

    public static bool isGameWon()
    {
        return gameWin;
    }

    public static bool isGameLost()
    {
        return gameLose;
    }

    public static void PauseGame()
    {
        gamePause = true;
        gameRunning = false;
    }

    public static void ResumeGame()
    {
        gamePause = false;
        gameRunning = true;
    }

    public static void SetLoseState()
    {
        gameLose = true;
        gameRunning = false;
        gameWin = false;
        
    }

    public static void SetWinState()
    {
        Debug.Log("Win");
        gameWin = true;
        gameRunning = false;
        gameLose = false;
    }

    public static void SetRitualActive()
    {
        ritualActive = true;
    }

    public static bool IsRitualActive()
    {
        return ritualActive;
    }

    public static void SetRitualComplete()
    {
        ritualComplete = true;
    }

    void addItem()
    {
        itemCount += 1;
    }

    void subtractItem()
    {
        itemCount -= 1;
    }

}
