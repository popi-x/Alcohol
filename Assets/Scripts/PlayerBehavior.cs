using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public int capacity;
    public int diceRoll;
    public string item;


    public GameObject startBtn;
    public GameObject rollDiceBtn;

    public void StartGame()
    {
        startBtn.SetActive(false);
        rollDiceBtn.SetActive(true);
        BattleManager.Instance.StartGame();
    }

    public void RollDice()
    {
        int playerDiceRoll = Random.Range(1, 7) * 10;
        BattleManager.Instance.SetPlayerDR(playerDiceRoll);
        Debug.Log("Player rolled: " + playerDiceRoll);
    }

}
