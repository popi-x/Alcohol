using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public BaseItem item;
    public int quanitity;

    public InventorySlot(BaseItem item, int n)
    {
        this.item = item;
        quanitity = n;
    }

}
