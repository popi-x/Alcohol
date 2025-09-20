using UnityEditor;
using UnityEngine;

public abstract class PlayerItem: BaseItem
{
    public Enemy enemy;

    private void Start()
    {
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
    }

    public abstract void Use(Enemy enemy);  
}
