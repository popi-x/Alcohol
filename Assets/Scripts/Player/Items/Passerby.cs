using UnityEngine;



[CreateAssetMenu(fileName = "Passerby", menuName = "Player Items/Passerby")]
public class Passerby : PlayerItem
{

    public override void Use(Enemy enemy)
    {
        Player player = Object.FindFirstObjectByType<Player>();
        int randIndex = Random.Range(0, enemy.inventory.itemSlots.Count);
        InventorySlot slot = enemy.inventory.itemSlots[randIndex];
        player.enemyItemInfo.Add(slot);
        Debug.Log("The passerby peeped that the enemy had a " + slot.item.itemName + " x " + slot.quantity);
    }
}
