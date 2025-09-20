using UnityEngine;


[CreateAssetMenu(fileName = "ThiefCall", menuName = "User Items/ThiefCall")]
public class ThiefCall : PlayerItem
{
    public override void Use(Enemy enemy)
    {
        return;
    }
}
