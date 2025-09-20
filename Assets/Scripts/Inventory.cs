using UnityEngine;
using System.Collections.Generic;

public class Inventory
{
    public List<InventorySlot> slots;

    public BaseItem RemoveItem(BaseItem item)
    {
        foreach (var slot in slots)
        {
            if (slot.item == item)
            {
                slot.quanitity--;
                if (slot.quanitity <= 0)
                {
                    slots.Remove(slot);
                }
                return item;
            }
        }
        return null;
    }

    public void AddItem(BaseItem item, int n=1)
    {
        foreach (var slot in slots)
        {
            if (slot.item == item)
            {
                slot.quanitity++;
                return;
            }
        }
        InventorySlot newInv = new InventorySlot(item, n);
        slots.Add(newInv);
    }

}
