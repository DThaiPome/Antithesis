using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragMonster : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform returnSpace;
    public bool unlocked;
    public GameObject monster;
    public Camera nonVRCamera;
    public WitchStatBlock wsb;
    private GameObject player;
    private Color color;
    private float distance;
    public float distanceFromPlayer = 10f;
    public int currentMana;
    public int cost;

    private void Start()
    {
        currentMana = wsb.ShowMana();
        this.player = GameObject.FindWithTag("Player");
        GetComponent<Image>().color = color;
    }

    private void Update()
    {
        currentMana = wsb.ShowMana();
        GetComponent<Image>().color = color;

        if (unlocked && (cost <= currentMana))
        {
            color = Color.green;
            color.a = 0.5f;
            transform.Rotate(0, 0, -45f * Time.deltaTime);

        }
        else if (unlocked && !(cost <= currentMana))
        {
            color = Color.white;
            color.a = 0.5f;
            transform.rotation = new Quaternion(0, 0, 45, 0);
        }

        else
        {
            color = Color.red;
            color.a = 0.5f;
            //transform.Rotate(0, 0, 0 * Time.deltaTime);
            transform.rotation = new Quaternion(0, 0, 45, 0);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {


    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log ("OnDrag");

        if (GameManager.isGameRunning() && this.unlocked && (cost <= currentMana))
        {
            this.transform.position = eventData.position;
        }
        

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GameManager.isGameRunning() && this.unlocked && (cost <= currentMana))
        {
            Debug.Log("OnEndDrag");
            RaycastHit hit;
            Ray ray = nonVRCamera.ScreenPointToRay(this.transform.position);

            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask.GetMask("Ground")))
            {

                distance = Vector3.Distance(hit.point, player.transform.position);

                if (distance > distanceFromPlayer && (currentMana >= cost))
                {
                    wsb.SpendMana(cost);
                    Instantiate(monster, hit.point + new Vector3(0, 1f, 0), Quaternion.identity);
                    this.transform.position = new Vector2(returnSpace.position.x + 103, returnSpace.position.y);
                }
            }
            //this.transform.position = new Vector2(returnSpace.position.x, returnSpace.position.y);

        }
       
    }

    public void Unlock()
    {
        this.unlocked = true;
    }
            
}