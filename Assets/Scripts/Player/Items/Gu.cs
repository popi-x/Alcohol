using UnityEngine;



[CreateAssetMenu(fileName = "Gu", menuName = "Player Items/Gu")]
public class Gu : PlayerItem
{
    public int guMtpler = 2;
    public int upgradeMtpler = 3;

    public override void Use(Enemy enemy)
    {
        enemy.guMtpler = guMtpler;
        enemy.hasGu = true;
    }

    public override void Upgrade()
    {
        guMtpler = upgradeMtpler;
        base.Upgrade();
    }
}
