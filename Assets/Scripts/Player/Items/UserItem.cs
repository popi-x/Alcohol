using UnityEngine;

public abstract class UserItem: ScriptableObject
{
    public Enemy enemy;

    private void Start()
    {
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
    }

    public abstract void Use(Enemy enemy);  
}
