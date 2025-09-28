using UnityEngine;

[CreateAssetMenu(fileName = "Poison", menuName = "Player Items/Poison")]
public class Poison : PlayerItem
{
    
    public override void Use(Enemy enemy)
    {
        if (Random.value < 0.05f)
        {
            enemy.cap = enemy.maxCap;
        }
        else
        {
            BattleManager.instance.playerWin = 0; 
        }   
    }
}
