using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{

    public int diceRoll;
    public float cap = 0f;
    public float maxCap = 100f;

    public bool lastConsent { get; set; } = true;
    //Todo:
    //Skills
    public List<string> skillSet { get; set; } = new List<string>() { "skill1", "skill2", "skill3" }; //temp
    //Items
    public List<UserItem> itemSet = new List<UserItem>();

    public void ApplyDamage()
    {
        return;
    }
}
