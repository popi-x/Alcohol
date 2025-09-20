using UnityEngine;

[CreateAssetMenu(fileName = "TempItem", menuName = "Enemy Item/TempItem")]
public class TempItem : EnemyItem
{
    public override void Use(Player player)
    {
        return;
    }
}
