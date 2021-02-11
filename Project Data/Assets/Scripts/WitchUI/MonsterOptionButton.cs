using UnityEngine;
using UnityEngine.UI;

public class MonsterOptionButton : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button monsterButton;
    public bool active;
    public bool unlocked;
    public int monsterNumber;

    public MonsterButtonManager mbm;

    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        monsterButton.onClick.AddListener(TaskOnClick);
        //var colors = GetComponent<Button>().colors;

        var colors = GetComponent<Button>().colors;
        colors.normalColor = Color.gray;
        GetComponent<Button>().colors = colors;

    }

    void TaskOnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
    }


    public void SetActive()
    {
        active = true;
    }

    public void SetInactive()
    {
        active = false;
    }

    public void Unlock()
    {
        unlocked = true;
    }


}