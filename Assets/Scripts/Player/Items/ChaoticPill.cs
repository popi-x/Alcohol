using UnityEngine;



[CreateAssetMenu(fileName = "Confusion Pill", menuName = "Player Items/Confusion Pill")]
public class ConfusionPill : PlayerItem
{
    
    public override void Use(Enemy enemy)
    {
        enemy.isConfused = true;
    }
}
