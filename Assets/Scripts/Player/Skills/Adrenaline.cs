using UnityEngine;


[CreateAssetMenu(fileName = "Adrenaline", menuName = "Skills/Player/Adrenaline")]
public class Adrenaline : BaseSkill
{
    public int adrenalineTurn = 1;

    public override void Use()
    {
        Player player = GameObject.FindFirstObjectByType<Player>();
        player.hasAdrenaline = true;
        player.adrenalineTurn = adrenalineTurn;
    }
}
