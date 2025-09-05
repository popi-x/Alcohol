using UnityEngine;

public class DEBomb : UserItem
{
    public string itemName = "Delayed Effect Bomb";
    public int DETurns = 2; //Starts in 2 turns, i.e. from current turn + 2
    public int DEMtpler = 3;

    

    public override void Use()
    {
        enemy.delayedEffectTurns += DETurns;
        enemy.delayedEffectMtpler = DEMtpler;
        
    } 
}
