using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
	public Button resetButton;

    // Start is called before the first frame update
    void Start()
    {
		resetButton.onClick.AddListener(TaskOnClick);
    }

 
    void TaskOnClick()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
