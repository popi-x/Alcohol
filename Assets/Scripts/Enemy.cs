using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public int diceRoll { get; set; }
    public int HP { get; set; } = 100;
    public int maxHP { get; set; } = 100;
    public bool lastConsent { get; set; } = false;
    public List<string> skillSets { get; set; } = new List<string>() { "skill1", "skill2", "skill3" }; //temp
    //Items
    public List<string> itemSets { get; set; } = new List<string>() { "item1", "item2", "item3" }; //temp 
    
    //Todo: Battle sequence
    
}
