using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTargetLocation : MonoBehaviour
{
    public GameManager GM;

    public ALightningSpell lightningSpell;

    public Camera nonVRCamera;
    public GameObject prefab;

    public WitchUI witchUIObject;
    public MonsterButtonManager mbm;
    public WitchStatBlock wsb;

    public GameObject rat;
    public GameObject gremlin;
    public GameObject wraith;
    public GameObject caster;
    public GameObject tank;

    public int currentMonster;
    public int currentMana;

    private int currentCost;

    private GameObject player;
    private float distance;
    public float distanceFromPlayer = 10f;
    public AMonsterPlaceRangeDisplay monsterPlaceRangeDisplay;

    private void Start()
    {
        currentMana = wsb.ShowMana();
        currentMonster = mbm.GetMonsterNumber();
        currentCost = mbm.GetManaCost();
        this.player = GameObject.FindWithTag("Player");

    }

    void Update()
    {
        currentMana = wsb.ShowMana();
        currentMonster = mbm.GetMonsterNumber();
        currentCost = mbm.GetManaCost();

        this.UpdatePlaceRangeMarker();


        if (Input.GetMouseButtonDown(0) && GameManager.isGameRunning())
        {

            Debug.Log("Left mouse clicked");
            RaycastHit hit;
            Ray ray = nonVRCamera.ScreenPointToRay(Input.mousePosition);


            if (!witchUIObject.IsUISelected() && Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask.GetMask("Enemy")))
            {
                if (hit.collider.gameObject.tag == "Rat" || hit.collider.gameObject.tag == "Goblin")
                {
                    Destroy(hit.collider.gameObject);
                    wsb.AddMana(1);
                }

                if (hit.collider.gameObject.tag == "Caster")
                {
                    Destroy(hit.collider.gameObject);
                    wsb.AddMana(2);
                }

                if (hit.collider.gameObject.tag == "Tank")
                {
                    Destroy(hit.collider.gameObject);
                    wsb.AddMana(3);
                }
            }

            //if (!witchUIObject.IsUISelected() && Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask.GetMask("Ground")))
            //{
            //    distance = Vector3.Distance(hit.point, player.transform.position);

            //    if (distance > distanceFromPlayer)
            //    {
            //        wsb.SpendMana(currentCost);

            //        if (currentMonster == 0)
            //        {
            //            Instantiate(rat, hit.point + new Vector3(0, 1f, 0), Quaternion.identity);
            //            print("My object is clicked by mouse");
            //        }

            //        if (currentMonster == 1)
            //        {
            //            Instantiate(gremlin, hit.point + new Vector3(0, 1f, 0), Quaternion.identity);
            //            print("My object is clicked by mouse");
            //        }

            //        if (currentMonster == 2)
            //        {
            //            Instantiate(wraith, hit.point + new Vector3(0, 1f, 0), Quaternion.identity);
            //            print("My object is clicked by mouse");
            //        }

            //        if (currentMonster == 3)
            //        {
            //            Instantiate(caster, hit.point + new Vector3(0, 1f, 0), Quaternion.identity);
            //            print("My object is clicked by mouse");
            //        }

            //        if (currentMonster == 4)
            //        {
            //            Instantiate(tank, hit.point + new Vector3(0, 1f, 0), Quaternion.identity);
            //            print("My object is clicked by mouse");
            //        }
            //    }
 

            //}
        }

        if (Input.GetMouseButtonDown(1) && GameManager.isGameRunning())
        {
            RaycastHit hit;
            Ray ray = nonVRCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask.GetMask("Ground")))
            {
                distance = Vector3.Distance(hit.point, player.transform.position);
                if (distance > distanceFromPlayer)
                {
                    this.lightningSpell.Cast(hit.point + new Vector3(0, .1f, 0));
                }
            }
        }
    }


    private void UpdatePlaceRangeMarker()
    {
        if (this.monsterPlaceRangeDisplay != null)
        {
            this.monsterPlaceRangeDisplay.SetRadius(this.distanceFromPlayer);
            this.monsterPlaceRangeDisplay.transform.position = this.player.transform.position;
        }
    }
}