using NUnit.Framework.Constraints;
using System;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private int playerDiceRoll;
    private int enemyDiceRoll;
    private int alcoholAmount;
    private bool playerTurn = true; // player always starts first i.e. enemy is the first one to drink
    private State crtState;
    

    public enum State
    {
        RollDice,
        Drink
    }
    public static BattleManager instance;
    public EnemyBehavior enemy; // Reference to the enemy behavior script
    public PlayerBehavior player; // Reference to the player behavior script

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
        switch (crtState)
        {
            case State.Drink:
                if (playerTurn)
                {
                    // Enemy drinks
                    UIController.instance.ShowItemSets();
                    UIController.instance.UpdateFB("It's your turn to prepare the drink");
                    playerTurn = false;
                }
                else
                {
                    // Player drinks
                    Debug.Log("Player drinks " + alcoholAmount + "ml");
                    playerTurn = true;
                }
                break;
        }
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
        crtState = State.Drink;
        UpdateState();
    }

    

}
