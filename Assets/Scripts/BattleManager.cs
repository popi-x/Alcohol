using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;
using System.Collections.Generic;
  
public enum battleState
    {
        PreRequisites,
        RollDice,
        UseItems,
        UseSkills,
        Decision,
        Result
    }

public class BattleManager : MonoBehaviour
{
    private int playerDiceRoll;
    private int enemyDiceRoll;
    private int alcoholAmount;
    private int maxSkillNum = 3;
    private int curSkillNum = 0;
    private List<PlayerItem> playerItemSet = new List<PlayerItem>(); //Reset
    private List<PlayerItem> usedItems = new List<PlayerItem>(); // to store player's used items in a turn;Reset
    private static BattleManager _instance;
    private int turnCnt = 0;

        
    public static BattleManager instance { get { return _instance; } private set { } }

    public Enemy enemy; // Reference to the enemy behavior script
    public Player player; // Reference to the player behavior script
    // Reference to the alchohol behavior script
    public Alcohol alcohol; 
    public battleState curState;
    public bool playerTurn = false; // player turn means player's turn to drink, i.e. enemy uses item/skill. Enemy is always the first to drink.


    public void Reset()
    {
        usedItems.Clear();
        playerItemSet.Clear();
    }


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
        curState = battleState.PreRequisites;

    }

    public void Start()
    {
        playerItemSet = player.itemSet;
    }

    private void Update()
    {
        

        switch (curState)
        {
            case battleState.PreRequisites:
                if (Input.GetKeyDown(KeyCode.N))
                {
                    Debug.Log("Check Prerequisites");
                    CheckPrerequisite();
                }
                break;
            case battleState.RollDice:
                if (Input.GetKeyDown(KeyCode.N))
                {
                    turnCnt++;
                    Debug.Log("Turn " + turnCnt);
                    //first check if any entity's debuff or buff takes effect at the start of turn
                    
                    enemy.ApplyStartOfTurnEffects();
                    Debug.Log("Enemy's cap after start of turn effects: " + enemy.cap);


                    Debug.Log("Roll Dice");
                    enemyDiceRoll = RollaDice();
                    enemy.diceRoll = enemyDiceRoll;

                    playerDiceRoll = RollaDice();
                    player.diceRoll = playerDiceRoll;

                    SumUpDiceResult();

                    Debug.Log("Enemy rolled: " + enemyDiceRoll);
                    Debug.Log("Player rolled: " + playerDiceRoll);
                    Debug.Log("Total alcohol damage: " + alcoholAmount);
                    curState = battleState.UseItems;
                }
                break;

            case battleState.UseItems:
                string msg = !playerTurn ? "Player's turn to use items" : "Enemy's turn to use items";
                if (Input.GetKeyDown(KeyCode.M))
                {
                    Debug.Log(msg);
                }

                if (!playerTurn)
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        usedItems.Add(playerItemSet[0]);
                        Debug.Log("Player uses item 1");
                    }
                    else if (Input.GetKeyDown(KeyCode.W))
                    {
                        usedItems.Add(playerItemSet[1]);
                        Debug.Log("Player uses item 2");
                    }
                    else if (Input.GetKeyDown(KeyCode.E))
                    {
                        usedItems.Add(playerItemSet[2]);
                        Debug.Log("Player uses item 3");
                    }
                    else if (Input.GetKeyDown(KeyCode.N))
                    {
                        curState = battleState.UseSkills; // move to next state
                    }
                }
                else
                {
                    Debug.Log("Enemy uses item 1");
                   curState = battleState.UseSkills; // move to next state
                }
                
                break;

            case battleState.UseSkills:
                msg = !playerTurn ? "Enemy's turn to use skills" : "Player's turn to use skills";
                if (Input.GetKeyDown(KeyCode.M))
                {
                    Debug.Log(msg);
                }

                if (playerTurn && curSkillNum != maxSkillNum)
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        UseSkill(1);
                        curSkillNum++;
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        UseSkill(2);
                        curSkillNum++;
                    }
                    else if (Input.GetKeyDown(KeyCode.D))
                    {
                        UseSkill(3);
                        curSkillNum++;
                    }
                    else if (Input.GetKeyDown(KeyCode.N))
                    {
                        curState = battleState.Decision; // move to next state
                    }
                }
                else if (playerTurn && curSkillNum == maxSkillNum)
                {
                    curState = battleState.Decision; // move to next state
                }
                else
                {
                    UseSkill(1); // enemy uses skill  1 for now
                    Debug.Log("Enemy uses skill 1");
                    curState = battleState.Decision; // move to next state
                }
                break;
                

            case battleState.Decision:
                msg = !playerTurn ? "Enemy's turn to decide" : "Player's turn to decide";
                if (Input.GetKeyDown(KeyCode.M))
                {
                    Debug.Log(msg);
                }
                if (playerTurn)
                {
                    if (Input.GetKeyDown(KeyCode.Y))
                    {
                        MakeADecision(true);
                        curState = battleState.Result;
                    }
                    else if (Input.GetKeyDown(KeyCode.N))
                    {
                        MakeADecision(false);
                        curState = battleState.Result;
                    }
                }
                else
                {
                    MakeADecision(true);
                    curState = battleState.Result;
                }
                    break;
            case battleState.Result:
                playerTurn = !playerTurn; // switch turn
                curState = battleState.RollDice;

                break;
            
            default:
                break;
        }
    }

    


    /* public void UpdateState(battleState newState)
     {
         curState = newState;

         switch(curState)
         {
             case battleState.PreRequisites:
                 CheckPrerequisite();
                 UpdateState(battleState.RollDice);
                 break;

             case battleState.RollDice:
                 Debug.Log("Roll Dice");
                 enemyDiceRoll = RollaDice();
                 enemy.diceRoll = enemyDiceRoll;

                 playerDiceRoll = RollaDice();
                 player.diceRoll = playerDiceRoll;

                 SumUpDiceResult();

                 Debug.Log("Enemy rolled: " + enemyDiceRoll);
                 Debug.Log("Player rolled: " + playerDiceRoll);
                 Debug.Log("Total alcohol damage: " + alcoholAmount);
                 UpdateState(battleState.UseItems);
                 break;

             case battleState.UseItems:
                 UseItems();
                 UpdateState(battleState.UseSkills);
                 break;

             case battleState.UseSkills:
                 UseSkills();
                 UpdateState(battleState.Decision);
                 break;

             case battleState.Decision:
                 MakeDeicision();
                 UpdateState(battleState.Result);

             case battleState.Result:
                 CalculateDamageResult();
                 Debug.Log("Calculating Result");
                 playerTurn = !playerTurn; // switch turn
                 UpdateState(battleState.RollDice);
                 break;
         }

     }*/



    void CheckPrerequisite()
    {
        // check if player has something enemy needs
        // tips - change state
        curState = battleState.RollDice;
    }

    int RollaDice()
    {
        // return a dice result to a entity(player or enemy)
        return Random.Range(1, 7) * 10;
    }

     
    void SumUpDiceResult()
    {
        // return a sum
        // alchohol's damage
        alcoholAmount = playerDiceRoll + enemyDiceRoll;
    }

    


    void UseSkill(int index)
    {
        Debug.Log("uses skill" + index);

    }

    void MakeADecision(bool decision)
    {
        if (playerTurn)
        {
            
            if (decision)
            {
                player.lastConsent = true;
                Debug.Log("Player chooses to drink");
                CalculateDamageResult();
            }
            else
            {
                if (player.lastConsent == false)
                {
                    Debug.Log("player can't reject this time");
                    player.lastConsent = true;
                    CalculateDamageResult();
                }
                Debug.Log("Player chooses not to drink");
            } 
        }
        else
        {
           // enemy decision making
            if (decision)
            {
                enemy.lastConsent = true;
                Debug.Log("Enemy chooses to drink");
                CalculateDamageResult();
            }
            else
            {
                if (enemy.lastConsent == false)
                {
                    Debug.Log("Enemy can't reject this time");
                    enemy.lastConsent = true;
                    CalculateDamageResult();
                }
                Debug.Log("Enemy chooses not to drink");
            }
        }
    }

    

    void CheckEntityHP()
    {
        // check if proceed or loop
    }


    private void CalculateDamageResult() //only called when the entity chooses to drink
    {
        var alcoholDmg = alcoholAmount * alcohol.conversion;

        //check who's turn 
        if (!playerTurn) //enemy's turn to drinK
        {   
            if (enemy.lastConsent)
            {
                enemy.pendingDamage += alcoholDmg;
                foreach (PlayerItem item in usedItems)
                {
                    item.Use(enemy);
                }
            }
            enemy.ApplyDamage();
            Debug.Log("enemy's cap: " + enemy.cap);
        }
        else
        {
            if (player.lastConsent)
            {
                player.cap += alcoholDmg;
            }
            player.ApplyDamage();
            Debug.Log("player's cap: " + player.cap);
        }

        usedItems.Clear();

    }
    



    
}
