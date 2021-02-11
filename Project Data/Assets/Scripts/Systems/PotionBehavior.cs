using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBehavior : MonoBehaviour
{
    bool eaten;
    bool held;
    public GameObject head;
    public float distToEat;

    public GameObject player;

    public enum statToChange {
        hp,
        maxHp,
        dex
    }

    public int changeAmount;
    public statToChange stat;


    // Start is called before the first frame update
    void Start()
    {
        GameObject[] heads = GameObject.FindGameObjectsWithTag("Player Camera");
        foreach(GameObject head in heads)
        {
            if (head.activeInHierarchy)
            {
                this.head = head;
                break;
            }
        }

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            if (player.activeInHierarchy)
            {
                if (player.GetComponent<AStatBlock>() != null)
                {
                    this.player = player;
                    break;
                }
            }
        }

        eaten = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Get held in hand
        held = true;

        //When potion models are added, add qualifier for rotation to make sure that players rotate to pour rather than eating the whole thing.
        if (!eaten && Vector3.Distance(transform.position, head.transform.position) < distToEat)
        {
            eaten = true;

            // Do Something to player and object
            transform.localScale -= new Vector3(.1f, .1f, .1f);
            Debug.Log("EATEN");

            switch(stat)
            {
                case statToChange.hp:
                    player.GetComponent<StatBlock>().ModifyHP(changeAmount);
                    break;
                case statToChange.maxHp:
                    //NOT IMPLEMENTED
                    //player.GetComponent<StatBlock>().AddBuff(new ExtraArmorBuff(changeAmount));
                    break;
                case statToChange.dex:
                    player.GetComponent<StatBlock>().AddBuff(new ExtraDexBuff(changeAmount));
                    break;

            }
            
        }
    }
}
