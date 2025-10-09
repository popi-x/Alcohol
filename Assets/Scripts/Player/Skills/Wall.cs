using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Wall", menuName = "Skills/Player/Wall")]
public class Wall: BaseSkill
{
    public float reductionAmt = 10f;
    public override void Use(params Object[] entities)
    {
        Player player = GameObject.FindFirstObjectByType<Player>();
        player.cap -= reductionAmt;
    }
}