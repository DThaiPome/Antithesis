using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class WitchUI : MonoBehaviour
{
    [SerializeField]
    private List<WitchUIElement> buttons;

    public bool IsUISelected()
    {
        return this.CheckIfButtonsSelected();
    }

    private bool CheckIfButtonsSelected()
    {
        foreach(WitchUIElement b in this.buttons)
        {
            if (b.IsSelected())
            {
                return true;
            }
        }
        return false;
    }
}
