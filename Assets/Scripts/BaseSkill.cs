using UnityEngine;

public enum skillType
{
   Attack,
   Defend,
   Technique
}


public abstract class BaseSkill : ScriptableObject
{
    public string skillName;
    public int coolDown;
    public skillType type;
    public abstract void Use();
}
