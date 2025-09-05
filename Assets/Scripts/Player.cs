using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    public int diceRoll { get; set; }
    public int HP { get; set; } = 100;
    public int maxHP { get; set; } = 100;

    public bool lastConsent { get; set; } = false;
    //Todo:
    //Skills
    public List<string> skillSet { get; set; } = new List<string>() { "skill1", "skill2", "skill3" }; //temp
    //Items
    public List<UserItem> itemSet = new List<UserItem>();

}
