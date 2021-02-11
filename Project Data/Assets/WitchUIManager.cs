using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchUIManager : MonoBehaviour
{
    public GameObject witchPlayUI;
    public GameObject witchLoseUI;
    public GameObject witchWinUI;

    public GameObject playerLoseUI;
    public GameObject playerWinUI;

    // Start is called before the first frame update
    void Start()
    {
        witchPlayUI.SetActive(true);
        witchLoseUI.SetActive(false);
        witchWinUI.SetActive(false);

        playerLoseUI.SetActive(false);
        playerWinUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGameWon())
        {
            witchLoseUI.SetActive(true);
            witchPlayUI.SetActive(false);
            witchWinUI.SetActive(false);

            playerLoseUI.SetActive(false);
            playerWinUI.SetActive(true);
        }

        if (GameManager.isGameLost())
        {
            witchWinUI.SetActive(true);
            witchLoseUI.SetActive(false);
            witchPlayUI.SetActive(false);

            playerLoseUI.SetActive(true);
            playerWinUI.SetActive(false);
        }

        if (GameManager.gameRunning)
        {
            playerLoseUI.SetActive(false);
            playerWinUI.SetActive(false);
        }
    }
}
