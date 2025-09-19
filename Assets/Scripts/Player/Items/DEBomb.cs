using UnityEngine;

[CreateAssetMenu(fileName = "DE Bomb", menuName = "Items/DE Bomb")]
public class DEBomb : UserItem
{
    public string itemName = "Delayed Effect Bomb";
    public int DETurns = 2; //Starts in 2 turns, i.e. from current turn + 2
    public int DEMtpler = 3;

    

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
                enemy.DEHandler.curState = DEHandler.DEState.Active;
            }
            else
            {
                enemy.DEHandler.remainingTurns += DETurns;
            }
        }

    } 
}
