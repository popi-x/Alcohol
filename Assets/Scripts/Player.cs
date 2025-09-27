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

    public bool lastConsent { get; set; } = true;
    //Todo:

    public void ApplyDamage()
    {
        return;
    }
}
