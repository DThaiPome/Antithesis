using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    public Text gameover;
    // Start is called before the first frame update
    void Start()
    {
        gameover.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGameWon())
        {
            gameover.text = "Sorry, You Lost!";
        }

        if (GameManager.isGameLost())
        {
            gameover.text = "Congrats, You Won!";
        }
    }
}
