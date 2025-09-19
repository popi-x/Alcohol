using UnityEngine;



[CreateAssetMenu(fileName = "Vitamin C", menuName = "Items/Vitamin C")]
public class VitaminC : UserItem
{
    public string itemName = "Vitamin C";
    public int icmt = 7;


    

    public override void Use(Enemy enemy)
    {
        enemy.pendingDamage += icmt;
    }
}
