using UnityEngine;

[CreateAssetMenu(fileName = "DE Bomb", menuName = "User Items/DE Bomb")]
public class DEBomb : PlayerItem
{
    public int DETurns = 3; //Starts in 3 turns, i.e. from current turn + 3
    public int DEMtpler = 3;
    public override int priority => 1;

    public override void Use(Enemy enemy)
    {         
        if (enemy.DEHandler == null) {
            Debug.LogError("Enemy does not have a DEHandler component.");
        }
        else
        {
            if (enemy.DEHandler.curState == DEHandler.DEState.Inactive)
            {
                enemy.DEHandler.Init(enemy, DETurns, DEMtpler);
                enemy.DEHandler.bufferState.Add(DEHandler.DEState.Active);
            }
            else
            {
                enemy.DEHandler.remainingTurns += DETurns;
            }
        }

    } 
}
