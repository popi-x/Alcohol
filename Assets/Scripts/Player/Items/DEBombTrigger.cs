using UnityEngine;

public class DEBombTrigger : UserItem
{
    public string itemName = "DEBombTrigger";
    
    public override void Use()
    {
        enemy.DETriggered = !enemy.DETriggered;
    }
    

}
