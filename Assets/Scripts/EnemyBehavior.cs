using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int healthPoint;
    public int capacity;
    public int diceRoll;
    public int RollDice()
    {
        int enemyDiceRoll = Random.Range(1, 7) * 10; // Simulating a dice roll
        Debug.Log("Enemy rolled: " + enemyDiceRoll);
        return enemyDiceRoll;
    } 
}
