using UnityEngine;

public abstract class EnemyItem : BaseItem
{
    public Player player;
    
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public abstract void Use(Player player);
}
