using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

public class Enemy : MonoBehaviour
{
    //激进程度int
    public int diceRoll;
    public float cap = 0f;
    public float maxCap = 800f;
    public float pendingDamage = 0f; //All damage to be applied
    public int silentTurns = 0;
    public bool lastConsent { get; set; } = false;
    public bool hasGu = false;
    public bool isConfused = false;


    //Delayed Effect will start in x turns. First check if it's triggered, and then add turns number from the item after effect is triggered
    public DEHandler DEHandler;
    public Inventory inventory = new Inventory(5);
    public Player player;
    
    //Todo: Battle sequence
    //public void 

    public void Awake()
    {
        DEHandler = new DEHandler();
    }

    public void UseItem()
    {
        if (isConfused && Random.value < 0.5f)
        {
            //50% chance to use a random item
            if (inventory.itemSlots.Count > 0)
            {
                //int randomIndex = Random.Range(0, inventory.itemSlots.Count);
                //inventory.itemSlots[randomIndex].UseItem(this);
                Debug.Log("Enemy used a random item due to Confusion state.");
            }
        }
        else
        {
            return;
        }
    }

    public void UseSkill()
    {
        if (silentTurns == 0)
        {
            Debug.Log("Enemy used a skill.");
        }
        else
        {
            Debug.Log("Enemy is silent and cannot use skills.");
            silentTurns--;
        }
    }


    public void ApplyStartOfTurnEffects()
    {
        if (DEHandler.curState == DEHandler.DEState.Exploding)
        {
            DEHandler.Execute();
        }
        else if (DEHandler.curState == DEHandler.DEState.Active)
        {
            if (!BattleManager.instance.playerTurn)
            {
                DEHandler.Execute(keepTurn: true);
            }
            else
            {
                DEHandler.Execute(); 
            }
        }
        Debug.Log("DE turns remaining: " + DEHandler.remainingTurns);
        cap += pendingDamage;
        pendingDamage = 0f;
    }


    public void ApplyDamage()
    {
        if (hasGu)
        {
            pendingDamage = 2 * player.pendingDamage;
            hasGu = false;
        }        

        if (DEHandler.curState != DEHandler.DEState.Inactive)
        {
            DEHandler.Execute();
        }
        Debug.Log("DE turns remaining: " + DEHandler.remainingTurns);
        cap += pendingDamage;
        pendingDamage = 0f;

    }



}
