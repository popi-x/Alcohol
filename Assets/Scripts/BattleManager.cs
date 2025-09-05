using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;
using System.Collections.Generic;
  

public class BattleManager : MonoBehaviour
{
    private int playerDiceRoll;
    private int enemyDiceRoll;
    private int alcoholAmount;
    private bool playerTurn = false; // player always starts first i.e. enemy is the first one to drink
    private State curState;
    private int maxSkillNum = 3;
    private int curSkillNum = 0;
    private List<UserItem> playerItemSet = new List<UserItem>();
    private List<UserItem> usedItems = new List<UserItem>(); // to store player's used items in a turn

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

    public Enemy enemy; // Reference to the enemy behavior script
    public Player player; // Reference to the player behavior script
    // Reference to the alchohol behavior script
    public Alcohol alcohol;

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
                    Debug.Log("Check Prerequisites");
                    CheckPrerequisite();
                }
                break;
            case State.RollDice:
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    Debug.Log("Roll Dice");
                    enemyDiceRoll = RollaDice();
                    enemy.diceRoll = enemyDiceRoll;

                    playerDiceRoll = RollaDice();
                    player.diceRoll = playerDiceRoll;

                    SumUpDiceResult();

                    Debug.Log("Enemy rolled: " + enemyDiceRoll);
                    Debug.Log("Player rolled: " + playerDiceRoll);
                    Debug.Log("Total alcohol damage: " + alcoholAmount);
                    curState = State.UseItems;
                }
                break;

            case State.UseItems:
                if (playerTurn)
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        //??how to let UseItemToAlchohol know who's using items??
                        usedItems.Add(playerItemSet[0]);
                    }
                    else if (Input.GetKeyDown(KeyCode.W))
                    {
                        usedItems.Add(playerItemSet[1]);
                    }
                    else if (Input.GetKeyDown(KeyCode.E))
                    {
                        usedItems.Add(playerItemSet[2]);
                    }
                    else if (Input.GetKeyDown(KeyCode.R))
                    {
                        curState = State.UseSkills; // move to next state
                    }
                }
                else
                {
                   UseItemToAlchohol(1); // enemy uses item 1 for now 
                    Debug.Log("Enemy uses item 1");
                   curState = State.UseSkills; // move to next state
                }
                
                break;

            case State.UseSkills:
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
                    else if (Input.GetKeyDown(KeyCode.R))
                    {
                        curState = State.Decision; // move to next state
                    }
                }
                else if (playerTurn && curSkillNum == maxSkillNum)
                {
                    curState = State.Decision; // move to next state
                }
                else
                {
                    UseSkill(1); // enemy uses skill  1 for now
                    Debug.Log("Enemy uses skill 1");
                    curState = State.Decision; // move to next state
                }
                break;
                

            case State.Decision:
                if (playerTurn)
                {
                    if (Input.GetKeyDown(KeyCode.Y))
                    {
                        MakeADecision(true);
                        curState = State.Result;
                    }
                    else if (Input.GetKeyDown(KeyCode.N))
                    {
                        MakeADecision(false);
                        curState = State.Result;
                    }
                }
                else
                {
                    MakeADecision(true);
                    curState = State.Result;
                }
                    break;
            case State.Result:
                CalculateDamageResult();
                Debug.Log("Calculating Result");
                playerTurn = !playerTurn; // switch turn
                curState = State.RollDice;

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
    }

     
    void SumUpDiceResult()
    {
        // return a sum
        // alchohol's damage
        alcoholAmount = playerDiceRoll + enemyDiceRoll;
    }

    void UseItemToAlchohol(int index)
    {
       Debug.Log("uses item" + index);  
    }


    void UseSkill(int index)
    {
        // get a defense
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

    void CalculateDamageResult()
    {
        if (playerTurn)
        {
            foreach (var item in usedItems)
            {
                item.Use();
            }
            if (enemy.delayedEffectTurns == 0)
            {
                enemy.cap += enemy.pendingDamage;
            }
            else
            {
                if 
            }

        }
    }

    void CheckEntityHP()
    {
        // check if proceed or loop
    }

    



    
}
