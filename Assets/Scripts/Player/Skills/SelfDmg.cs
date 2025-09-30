using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SelfDmg", menuName = "Skills/Player/SelfDmg")]
public class SelfDmg: BaseSkill
{
    public float dmgAmt = 20;

    public override void Use()
    {
        Player player = GameObject.FindFirstObjectByType<Player>();
        player.pendingDamage += dmgAmt;
    }
}