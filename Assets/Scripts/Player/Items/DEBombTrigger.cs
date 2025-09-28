using UnityEngine;

[CreateAssetMenu(fileName = "DE Bomb Trigger", menuName = "Player Items/DE Bomb Trigger")]
public class DEBombTrigger : PlayerItem
{
    public override int priority => 2;
     
    public override void Use(Enemy enemy)
    {
       if (enemy.DEHandler == null) {
            Debug.LogError("Enemy does not have a DEHandler component.");
        }
        else
        {
            if (enemy.DEHandler.curState == DEHandler.DEState.Active)
            {
                enemy.DEHandler.bufferState.Add(DEHandler.DEState.Triggered);
            }
            else
            {
                Debug.Log("DE Bomb Trigger used, but no active DE to trigger.");
            }
        }

    }
    

}
