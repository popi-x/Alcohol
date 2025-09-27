using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

public class Enemy : MonoBehaviour
{
    //激进程度int
    public int diceRoll;
    public float cap = 0f;
    public float maxCap = 800f;

    public bool lastConsent { get; set; } = false;

    public float pendingDamage = 0f; //All damage to be applied

    //Delayed Effect will start in x turns. First check if it's triggered, and then add turns number from the item after effect is triggered
    public DEHandler DEHandler;
    public Inventory inventory = new Inventory(5);

    //Todo: Battle sequence
    //public void 

    public void Awake()
    {
        DEHandler = new DEHandler();
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
        //Delayed Effect executes
        if (DEHandler.curState == DEHandler.DEState.Active)
        {
            DEHandler.Execute();
        }
        else if (DEHandler.curState == DEHandler.DEState.Triggered)
        {
            DEHandler.Execute();
        }
        Debug.Log("DE turns remaining: " + DEHandler.remainingTurns);
        cap += pendingDamage;
        pendingDamage = 0f;

    }



}
