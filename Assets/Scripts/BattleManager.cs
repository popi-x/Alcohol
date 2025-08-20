using NUnit.Framework.Constraints;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private int playerDiceRoll;
    private int enemyDiceRoll;
    private int alcoholAmount;
    private bool playerTurn = true; // player always starts first i.e. enemy is the first one to drink
    private State curState;

    public enum State
    {
        PreRequisites,
        RollDice,
        UseItems,
        UseSkills,
        Decision,
        Result
    }
    public static BattleManager Instance { get { return _instance; } private set { } }
    private static BattleManager _instance;

    public EnemyBehavior enemy; // Reference to the enemy behavior script
    public PlayerBehavior player; // Reference to the player behavior script

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(_instance);
        }
        curState = State.PreRequisites;

    }

    private void Update()
    {
        

        switch (curState)
        {
            case State.PreRequisites:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    CheckPrerequisite();
                }
                break;
            case State.RollDice:
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    enemyDiceRoll = RollaDice();

                    playerDiceRoll = RollaDice();

                    SumUpDiceResult();
                }
                break;
            default:
                break;
        }
    }

    void CheckPrerequisite()
    {
        // check if player has something enemy needs
        // true
        // tips - change state
        curState = State.RollDice;
        // false
    }

    int RollaDice()
    {
        // return a dice result to a entity(player or enemy)
        return Random.Range(1, 7) * 10;

        //BattleManager.Instance.SetPlayerDR(playerDiceRoll);
        //Debug.Log("Player rolled: " + playerDiceRoll);
    }

    void SumUpDiceResult()
    {
        // return a sum
        // alchohol's damage
        alcoholAmount = playerDiceRoll + enemyDiceRoll;
    }

    void UseItemToAlchohol()
    {
        // change the alchohols damage
    }

    void UseSkill()
    {
        // get a defense
    }

    void MakeADecision()
    {
        // decide drink or not
    }

    void CalculateDamageResult()
    {

    }

    void CheckEntityHP()
    {
        // check if proceed or loop
    }

    public void SetPlayerDR(int n)
    {
        playerDiceRoll = n;
        SumDR();
    }
    public void StartGame()
    { 
        SetEnemyDR(enemy.RollDice());      
    }
    public void UpdateState()
    {
        //switch (crtState)
        //{
        //    case State.Drink:
        //        if (playerTurn)
        //        {
        //            // Enemy drinks
        //            UIController.instance.ShowItemSets();
        //            UIController.instance.UpdateFB("It's your turn to prepare the drink");
        //            playerTurn = false;
        //        }
        //        else
        //        {
        //            // Player drinks
        //            Debug.Log("Player drinks " + alcoholAmount + "ml");
        //            playerTurn = true;
        //        }
        //        break;
        //}
    }     
    public void PlayersTurn()
    {
        return;
    }
    public void EnemysTurn()
    {
        return;
    }
    public void CalculateAttackVal()
    {
        return;
    }
    private void SetEnemyDR(int n)
    {
        enemyDiceRoll = n;
        UIController.instance.UpdateDiceRoll(n);
    }
    private void SumDR()
    {
        alcoholAmount = playerDiceRoll + enemyDiceRoll;
        UIController.instance.UpdateAmount(alcoholAmount);
        //crtState = State.Drink;
        UpdateState();
    }
}
