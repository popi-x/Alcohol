using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AgentEye", menuName = "Skills/Player/AgentEye")]
public class AgentEye: BaseSkill
{
    public override void Use()
    {
        List<InventorySlot> enemyUsedItem = BattleManager.instance.usedItems;
        foreach (InventorySlot slot in enemyUsedItem)
        {
            Debug.Log("Enemy used item: " + slot.item.itemName);
        }
    }
}