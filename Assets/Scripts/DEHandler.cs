using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class DEHandler
{
   public enum DEState
    {
        Inactive,
        Active,
        Triggered,
        Exploding
    }

    public DEState curState = DEState.Inactive;
    public List<DEState> bufferState = new List<DEState>();
    public int remainingTurns = -1;
    public float damageSum = 0f;
    public float multiplier = 0f;
    public float triggerMtpler = 0f;
    public bool triggerIsUpgraded = false;
    Enemy enemy;

    public void Init(Enemy e, int n, float m) 
    {
        remainingTurns = n; 
        enemy = e;
        multiplier = m;
        if (triggerIsUpgraded) triggerMtpler = multiplier + 1;
        else triggerMtpler = multiplier;
        curState = DEState.Active;
    }

    public void Execute(bool keepTurn = false) 
    {
        //Debug.Log("Current state:" + curState);
        if (bufferState.Count != 0)
        {
            foreach (var state in bufferState)
            {
                curState = state;
                RunState(keepTurn);

            }
        }
        else
        {
            RunState(keepTurn);
        }

        if (!keepTurn)
            remainingTurns -= 1;

        if (remainingTurns == -1 && curState == DEState.Exploding)
        {
            curState = DEState.Inactive;
            damageSum = 0f;
            multiplier = 0f;
            remainingTurns = -1;
        }
        if (remainingTurns == 0 && curState == DEState.Active)
        {
            curState = DEState.Exploding;
        }

        bufferState.Clear();
        
    }

    private void RunState(bool keepTurn = false)
    {
        switch (curState)
        {
            case DEState.Inactive:
                break;
            case DEState.Active:
                damageSum += enemy.pendingDamage;
                Debug.Log(damageSum + " damage stored in DEHandler.");
                enemy.pendingDamage = 0f;
                break;
            case DEState.Triggered:
                enemy.pendingDamage += damageSum * triggerMtpler; 
                Debug.Log("DE explodes: " + damageSum * triggerMtpler);
                curState = DEState.Exploding;
                break;
            case DEState.Exploding:
                enemy.pendingDamage += damageSum * multiplier;
                Debug.Log("DE explodes: " + damageSum * multiplier);
                break;
        }


    }

    


}
