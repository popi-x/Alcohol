using UnityEngine;

public abstract class UserItem: MonoBehaviour
{
    public Enemy enemy;

    private void Start()
    {
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
    }

    public abstract void Use();  
}
