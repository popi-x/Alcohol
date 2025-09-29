using UnityEngine;



[CreateAssetMenu(fileName = "Honey Trap", menuName = "Player Items/Honey Trap")]
public class HoneyTrap : PlayerItem
{

    public override void Use(Enemy enemy)
    {
        BattleManager.instance.doubleDrink = true;
    }
}
