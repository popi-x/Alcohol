using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public BaseItem item;
    public int quantity;

    public InventorySlot(BaseItem item, int n)
    {
        this.item = item;
        quantity = n;
    }

    public void UseItem(Enemy enemy)
    {
        if (quantity > 0)
        {
            (item as PlayerItem)?.Use(enemy);
            quantity--;
            Debug.Log("Player used " + item.itemName + ". Remaining quantity: " + quantity);
        }
        if (quantity == 0)
        {
            item = null;
        }
    }

}
