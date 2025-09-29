using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{

    public int diceRoll;
    public float cap = 0f;
    public float maxCap = 800f;
    public int money = 0;
    public Inventory inventory = new Inventory(10);
    public float pendingDamage = 0f;
    public List<InventorySlot> enemyItemInfo;  

    public bool lastConsent { get; set; } = true;
    //Todo:

    private void Start()
    {
        var src = Resources.LoadAll<PlayerItem>("Items");
        foreach (var item in src)
        {
            inventory.AddItem(item, 2);
        }
        
    }


    public void ApplyDamage()
    {
        cap += pendingDamage;
    }
}
