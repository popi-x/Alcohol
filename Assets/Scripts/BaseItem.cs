using UnityEngine; 

public abstract class BaseItem: ScriptableObject
{
    public string itemName;
    public virtual int priority => 100;

}

