using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterButtonManager : MonoBehaviour
{
    public MonsterOptionButton rat;
    public MonsterOptionButton gremlin;
    public MonsterOptionButton wraith;
    public MonsterOptionButton caster;
    public MonsterOptionButton tank;

    public DragMonster wraithDrag;
    public DragMonster casterDrag;
    public DragMonster tankDrag;

    public int currentManaCost;

    public int wraithTimer = 60;
    public int casterTimer = 60;
    public int tankTimer = 60;

    private IEnumerator mst;
  

    //0 - rat, 1 - gremlin, 2 - wraith, 3 - caster, 4 - tank
    public int activeMonster;

    // Start is called before the first frame update
    void Start()
    {
        activeMonster = 0;
        mst = MonsterSpawnTimer();
        StartCoroutine(mst);
    }

    // Update is called once per frame
    void Update()
    {
        if (activeMonster == 0)
        {
            rat.SetActive();
            gremlin.SetInactive();
            wraith.SetInactive();
            caster.SetInactive();
            tank.SetInactive();
            currentManaCost = 1;
        }

        if (activeMonster == 1)
        {
            rat.SetInactive();
            gremlin.SetActive();
            wraith.SetInactive();
            caster.SetInactive();
            tank.SetInactive();
            currentManaCost = 1;
        }

        if (activeMonster == 2)
        {
            rat.SetInactive();
            gremlin.SetInactive();
            wraith.SetActive();
            caster.SetInactive();
            tank.SetInactive();
            currentManaCost = 2;
        }

        if (activeMonster == 3)
        {
            rat.SetInactive();
            gremlin.SetInactive();
            wraith.SetInactive();
            caster.SetActive();
            tank.SetInactive();
            currentManaCost = 2;
        }

        if (activeMonster == 4)
        {
            rat.SetInactive();
            gremlin.SetInactive();
            wraith.SetInactive();
            caster.SetInactive();
            tank.SetActive();
            currentManaCost = 3;
        }

        if (!GameManager.isGameRunning())
        {
            StopCoroutine(mst);
        }
    }

    public int GetManaCost()
    {
        return currentManaCost;
    }

    public int GetMonsterNumber()
    {
        return activeMonster;
    }

    public void ChangeMonsterNumber(int number)
    {
        activeMonster = number;
    }

    IEnumerator MonsterSpawnTimer()
    {
        yield return new WaitForSeconds(wraithTimer);
        wraith.Unlock();
        wraithDrag.Unlock();
        yield return new WaitForSeconds(casterTimer);
        caster.Unlock();
        casterDrag.Unlock();
        yield return new WaitForSeconds(tankTimer);
        tank.Unlock();
        tankDrag.Unlock();
        StopCoroutine(mst);
    }
}
