using UnityEngine;



[CreateAssetMenu(fileName = "Vitamin C", menuName = "Player Items/Vitamin C")]
public class VitaminC : PlayerItem
{
     public int icmt = 7;

    

    public override void Use(Enemy enemy)
    {
        enemy.pendingDamage += icmt;
    }
}
