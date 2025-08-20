using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public GameObject startBtn;
    public GameObject rollDiceBtn;

    public void StartGame()
    {
        startBtn.SetActive(false);
        rollDiceBtn.SetActive(true);
        BattleManager.instance.StartGame();
    }

    public void RollDice()
    {
        int playerDiceRoll = Random.Range(1, 7) * 10;
        BattleManager.instance.SetPlayerDR(playerDiceRoll);
        Debug.Log("Player rolled: " + playerDiceRoll);
    }

}
