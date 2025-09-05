using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    //激进程度int
    public int diceRoll { get; set; }
    public float cap { get; set; } = 0f;
    public int maxCap { get; set; } = 100;
    public bool lastConsent { get; set; } = false;
    public List<string> skillSets { get; set; } = new List<string>() { "skill1", "skill2", "skill3" }; //temp
    //Items
    public List<string> itemSets { get; set; } = new List<string>() { "item1", "item2", "item3" }; //temp 

    public float pendingDamage { get; set; } = 0f;


    //Delayed Effect will start in x turns. First check if it's triggered, and then add turns number from the item after effect is triggered
    public int delayedEffectTurns { get; set; } = 0; //triggered when it reaches 1; minus 1 from the next turn
    public int delayedEffectSum { get; set; } = 0; 

    public int delayedEffectMtpler { get; set; } = 0; //Multiplier for delayed effect
    public bool DETriggered { get; set; } = false; //Whether the delayed effect is triggered by DRBombTrigger

    //Todo: Battle sequence
    //public void 


}
