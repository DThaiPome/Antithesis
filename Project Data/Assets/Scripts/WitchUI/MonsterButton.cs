using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonsterButton : MonoBehaviour
{
    public Button monsterMainButton;
    private bool buttonActive;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;

    public DragMonster rat;
    public DragMonster gremlin;
    public DragMonster wraith;
    public DragMonster caster;
    public DragMonster tank;



    void Start()
    {
        //Button btn = monsterButton.GetComponent<Button>();
        monsterMainButton.onClick.AddListener(TaskOnClick);
        //btn.onClick.AddListener(TaskOnClick);
        //buttonActive = false;
        //button1.gameObject.SetActive(true);
        //button2.gameObject.SetActive(true);
        //button3.gameObject.SetActive(true);
        //button4.gameObject.SetActive(true);
        //button5.gameObject.SetActive(true);

    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        ActivateButton();
      
    }

    void Update()
    {
        if (buttonActive)
        {

            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            button3.gameObject.SetActive(true);
            button4.gameObject.SetActive(true);
            button5.gameObject.SetActive(true);
            rat.gameObject.SetActive(true);
            gremlin.gameObject.SetActive(true);
            caster.gameObject.SetActive(true);
            wraith.gameObject.SetActive(true);
            tank.gameObject.SetActive(true);

        }
        else
        {

            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
            button3.gameObject.SetActive(false);
            button4.gameObject.SetActive(false);
            button5.gameObject.SetActive(false);
            rat.gameObject.SetActive(false);
            gremlin.gameObject.SetActive(false);
            caster.gameObject.SetActive(false);
            wraith.gameObject.SetActive(false);
            tank.gameObject.SetActive(false);

        }
    }

    void ActivateButton()
    {
        if (!buttonActive)
        {
            buttonActive = true;
        }

        else
        {
            buttonActive = false;
        }

        //buttonActive = true;

    }
}