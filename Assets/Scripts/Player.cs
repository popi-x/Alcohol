using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{

    public int diceRoll;
    public float cap = 0f;
    public float maxCap = 800f;
    public int money = 0;
    public Inventory inventory = new Inventory();

    public bool lastConsent { get; set; } = true;
    //Todo:
    //Skills
    public List<string> skillSet { get; set; } = new List<string>() { "skill1", "skill2", "skill3" }; //temp
    //Items
    public List<PlayerItem> itemSet = new List<PlayerItem>();

    public void ApplyDamage()
    {
        return;
    }
}
