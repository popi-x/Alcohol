using UnityEngine;


[CreateAssetMenu(fileName = "Reverse", menuName = "Skills/Player/Reverse")]
public class Reverse: BaseSkill
{

    public override void Use(params Object[] entities)
    {
        BattleManager.instance.reverse = true;
    }
}
