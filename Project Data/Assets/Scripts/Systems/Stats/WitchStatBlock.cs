using System.Threading;
using UnityEngine;

// Witch stat block that starts with initial values
public class WitchStatBlock : AWitchStatBlock
{
    [SerializeField]
    private int mana = 10;
    [SerializeField]
    private int maxManaLevel = 20;
    [SerializeField]
    private int initialManaLevel = 10;
    private float counter = 0;
    [SerializeField]
    private float time = 30;
    [SerializeField]
    private int manaIncrease = 5;
    [SerializeField]
    private int maxManaIncreases = 5;
    private int manaIncreases;

    void Start()
    {
        this.mana = this.initialManaLevel;

        EventChannel.current.onEnemyDestroyedEvent += this.OnEnemyDestroyed;
    }

    void OnDestroy()
    {
        EventChannel.current.onEnemyDestroyedEvent -= this.OnEnemyDestroyed;
    }

    // updates the Mana by 5 every 30 seconds
    void Update()
    {
        if (GameManager.isGameRunning())
        {
            // count that increases with time
            counter += Time.deltaTime;

            if (counter >= time)
            {
                // increases Mana by 5 every 30 seconds
                if (this.manaIncreases < this.maxManaIncreases)
                {
                    this.mana = manaModification(manaIncrease);
                    this.manaIncreases++;
                }

                // resets the count
                counter = 0;
            }
        }
    }

    // gets current Mana level
    public override int GetMana()
    {
        int currentMana = this.mana;
        return currentMana;
    }

    // caps Mana within [0, maxManaLevel]
    public override int ModifyMana(int dM)
    {
        return manaModification(dM);
    }

    private int manaModification(int dM)
    {
        this.mana = Mathf.Clamp(this.mana + dM, 0, this.maxManaLevel);
        return this.mana;
    }

    public void SpendMana(int cost)
    {
        this.mana -= cost;
    }

    public int ShowMana()
    {
        return mana;
    }

    public void AddMana(int x)
    {
        this.mana += x;
    }

    private void OnEnemyDestroyed(string tag)
    {
        if (tag == "Rat" || tag == "Goblin")
        {
            AddMana(1);
        }

        if (tag == "Caster")
        {
            AddMana(2);
        }

        if (tag == "Tank")
        {
            AddMana(3);
        }
    }

}
