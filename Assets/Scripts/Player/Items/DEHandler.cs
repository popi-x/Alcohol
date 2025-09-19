using UnityEngine;

public class DEHandler : MonoBehaviour
{
   public enum DEState
    {
        Inactive,
        Active,
        Triggered,
        Exploding
    }

    public DEState curState = DEState.Inactive;
    public int remainingTurns = -1;
    public float damageSum = 0f;
    public float multiplier = 0f;
    Enemy enemy;

    public void Init(Enemy e, int n, float m) 
    {
        remainingTurns = n; 
        enemy = e;
        multiplier = m;
    }

    public void Execute(bool keepTurn = false) 
    { 
        switch (curState)
        {
            case DEState.Inactive:
                break;
            case DEState.Active:
                damageSum += enemy.pendingDamage;
                Debug.Log(damageSum + " damage stored in DEHandler.");
                enemy.pendingDamage = 0f;
                if (!keepTurn)
                    remainingTurns -= 1;
                // Logic for when the DE is active
                break;
            case DEState.Triggered:
                enemy.pendingDamage += damageSum * multiplier;
                Debug.Log("DE explodes: " + damageSum * multiplier);
                curState = DEState.Exploding;
                remainingTurns = -1;
                break;
            case DEState.Exploding:
                enemy.pendingDamage += damageSum * multiplier;
                Debug.Log("DE explodes: " + damageSum * multiplier);
                remainingTurns = -1;
                break;
        }

    }

    private void Update()
    {
        if (remainingTurns == 0 && curState == DEState.Active)
        {
            curState = DEState.Exploding;
        }
        if (remainingTurns == -1 && curState != DEState.Inactive)
        {
            Debug.Log("DEHandler reset to Inactive");
            curState = DEState.Inactive;
            damageSum = 0f;
            enemy = null;
        } 
    }


}
