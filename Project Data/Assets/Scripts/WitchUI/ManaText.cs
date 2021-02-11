using UnityEngine;
using UnityEngine.UI;

public class ManaText : MonoBehaviour
{
    public Text mana;
    public WitchStatBlock witchStats;

    void Start()
    {
        //Text sets your text to say this message
        mana.text = "Mana: " + witchStats.GetMana();
    }

    void Update()
    {
        mana.text = "Mana: " + witchStats.GetMana();
    }
}