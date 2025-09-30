using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class SkillRuntime
{
   public BaseSkill skill;
   public int remainingCoolDown;
    
   public SkillRuntime(BaseSkill skill)
   {
       this.skill = skill;
       remainingCoolDown = 0;
   }
    
   public bool canUse => remainingCoolDown <= 0;
    
    public void Use()
    {
        if (canUse)
        {
            skill.Use();
            remainingCoolDown = skill.coolDown;
        }
        else
        {
            Debug.LogWarning($"Skill {skill.skillName} is on cooldown for {remainingCoolDown} more turns.");
        }
    }

    public void UpdateCoolDown(int n=1)
    {
        if (remainingCoolDown > 0)
            remainingCoolDown -= n;

    }
}
