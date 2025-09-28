using UnityEngine;



[CreateAssetMenu(fileName = "Gu", menuName = "Player Items/Gu")]
public class Gu : PlayerItem
{
    public Player player; 
    
    public override void Use(Enemy enemy)
    {
        enemy.hasGu = true;
    }
}
