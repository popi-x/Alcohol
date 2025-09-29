using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class Inventory
{
    public List<InventorySlot> itemSlots = new List<InventorySlot>();


    public Inventory(int n)
    {
        for (int i = 0; i < n; i++)
        {
            itemSlots.Add(new InventorySlot(null, 0));
        }
    }

    public BaseItem RemoveItem(BaseItem item)
    {
        foreach (var slot in itemSlots)
        {
            if (slot.item == item)
            {
                slot.quantity--;
                if (slot.quantity <= 0)
                {
                    itemSlots.Remove(slot);
                }
                return item;
            }
        }
        return null;
    }

    public BaseItem RemoveItem(int index)
    {
        if (index < 0 || index >= itemSlots.Count)
        {
            return null;
        }
        var slot = itemSlots[index];
        slot.quantity--;
        if (slot.quantity <= 0)
        {
            itemSlots.RemoveAt(index);
        }
        return slot.item;
    }

    public void AddItem(BaseItem item, int n=1)
    {
        foreach (var slot in itemSlots)
        {
            if (slot.item == item)
            {
                slot.quantity++;
                return;
            }
            if (slot.item == null)
            {
                slot.item = item;
                slot.quantity += n;
                return;
            }
        }
        InventorySlot newInv = new InventorySlot(item, n);
        itemSlots.Add(newInv);
    }

}
