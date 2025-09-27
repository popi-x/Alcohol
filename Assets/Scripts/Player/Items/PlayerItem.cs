using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public abstract class PlayerItem: BaseItem
{
    public Enemy enemy;    

    public abstract void Use(Enemy enemy);  
}
