using UnityEngine;

public class VitaminC : UserItem
{
    public string itemName = "Vitamin C";
    public int icmt = 7;


    

    public override void Use()
    {
        enemy.pendingDamage += icmt;
    }
}
