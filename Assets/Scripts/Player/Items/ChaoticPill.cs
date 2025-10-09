using UnityEngine;



[CreateAssetMenu(fileName = "Confusion Pill", menuName = "Player Items/Confusion Pill")]
public class ConfusionPill : PlayerItem
{
    public float confusionProb = 0.5f;
    public float upgradeProb = 0.7f;

    public override void Use(Enemy enemy)
    {
        enemy.confusionProb = confusionProb;
        enemy.isConfused = true;
    }
    
    public override void Upgrade()
    {
        confusionProb = upgradeProb;
        base.Upgrade();
    }
}
