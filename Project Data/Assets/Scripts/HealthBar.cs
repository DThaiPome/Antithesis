using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    private PlayerStatBlock p;

    private void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatBlock>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = 20;
        healthBar.value = p.GetHP();
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }

    public void Update()
    {
        SetHealth(p.GetHP());
    }
}
