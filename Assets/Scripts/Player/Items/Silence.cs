using UnityEngine;

[CreateAssetMenu(fileName = "Silence", menuName = "Player Items/Silence")]
public class Silence : PlayerItem
{
    public int turns = 2;

    public override void Use(Enemy enemy)
    {
        enemy.silentTurns += turns;
    }
}
