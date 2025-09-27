using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CallThief", menuName = "User Items/CallTheif")]
public class CallThief : PlayerItem
{
    int stolenAmt = 2;

    public override void Use(Enemy enemy)
    {
        List<InventorySlot> enemyItemSlots = enemy.inventory.itemSlots;

        for (int i=0; i<stolenAmt; i++)
        {
            if (enemyItemSlots.Count > 0)
            {
                int randIndex = UnityEngine.Random.Range(0, enemyItemSlots.Count);
                BaseItem stolenItem = enemy.inventory.RemoveItem(randIndex);
                if (stolenItem != null)
                {
                    Debug.Log("Stole " + stolenItem.itemName);
                }
                else
                {
                    Debug.Log("Enemy has no items to steal.");
                }
            }
        }        
    }
}
