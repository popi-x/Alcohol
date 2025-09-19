using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    //激进程度int
    public int diceRoll;
    public float cap = 0f;
    public float maxCap = 100;
    public bool lastConsent { get; set; } = false;
    public List<string> skillSets { get; set; } = new List<string>() { "skill1", "skill2", "skill3" }; //temp
    //Items
    public List<string> itemSets { get; set; } = new List<string>() { "item1", "item2", "item3" }; //temp 

    public float pendingDamage = 0f; //All damage to be applied

    //Delayed Effect will start in x turns. First check if it's triggered, and then add turns number from the item after effect is triggered
    public DEHandler DEHandler;

    //Todo: Battle sequence
    //public void 

    public void Awake()
    {
        DEHandler = gameObject.AddComponent<DEHandler>();
    }


    public void ApplyStartOfTurnEffects()
    {
        if (DEHandler.curState == DEHandler.DEState.Exploding)
        {
            DEHandler.Execute();
        }
        else if (DEHandler.curState == DEHandler.DEState.Active)
        {
            if (BattleManager.instance.playerTurn)
            {
                DEHandler.Execute(true);
            }
            else
            {
                DEHandler.Execute(); //true indicates start of turn call
            }
        }
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
        cap += pendingDamage;
        pendingDamage = 0f;

    }


}
