using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

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
    private List<InventorySlot> playerItems;
    private List<InventorySlot> enemyItems;
    private List<SkillRuntime> playerSkills;
    private List<SkillRuntime> enemySkills;
    private static BattleManager _instance;
    private int turnCnt = 0;

        
    public static BattleManager instance { get { return _instance; } private set { } }

    public Enemy enemy; // Reference to the enemy behavior script
    public Player player; // Reference to the player behavior script
    // Reference to the alchohol behavior script
    public Alcohol alcohol; 
    public battleState curState;
    public List<InventorySlot> usedItems { get; private set; } = new List<InventorySlot>();

    public bool playerTurn = false; // player turn means player's turn to drink, i.e. enemy uses item/skill. Enemy is always the first to drink.
    public bool doubleDrink = false;
    public bool reverse = false; //enemy drinks in this turn instead of player
    public int playerWin = -1; // 1 means player win, 0 means enemy win, -1 means ongoing battle

    public void Reset()
    {
        enemyItems.Clear();
        usedItems.Clear();
        turnCnt = 0;
        curSkillNum = 0;
        playerWin = -1;
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
        playerItems = player.inventory.itemSlots;
        playerSkills = player.skills;
        enemyItems = enemy.inventory.itemSlots;
        enemySkills = enemy.skills;
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
                    //Reset some variables
                    curSkillNum = 0;

                    turnCnt++;
                    Debug.Log("Turn " + turnCnt);
                    //first check if any entity's debuff or buff takes effect at the start of turn

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
                    string msg = !playerTurn ? "Player's turn to use items" : "Enemy's turn to use items";
                    Debug.Log(msg);
                }
                break;

            case battleState.UseItems:
                if (!playerTurn)
                {
                    Dictionary<KeyCode, InventorySlot> itemKeyMap = new Dictionary<KeyCode, InventorySlot>()
                    {
                        { KeyCode.Alpha1, playerItems[0] },
                        { KeyCode.Alpha2, playerItems[1] },
                        { KeyCode.Alpha3, playerItems[2] },
                        { KeyCode.Alpha4, playerItems[3] },
                        { KeyCode.Alpha5, playerItems[4] },
                        { KeyCode.Alpha6, playerItems[5] },
                        { KeyCode.Alpha7, playerItems[6] },
                        { KeyCode.Alpha8, playerItems[7] },
                        { KeyCode.Alpha9, playerItems[8] },
                        { KeyCode.Alpha0, playerItems[9] },
                    };

                    foreach (var entry in itemKeyMap)
                    {
                        if (Input.GetKeyDown(entry.Key))
                        {
                            usedItems.Add(entry.Value);
                        }
                    }
                    
                    if (Input.GetKeyDown(KeyCode.N))
                    {
                        curState = battleState.UseSkills; // move to next state
                        Debug.Log("Enemy's turn to use skills");
                    }
                }
                else
                {
                    enemy.UseItem();
                    curState = battleState.UseSkills;
                    if (player.enemyItemInfo.Count > 0)
                    {
                        foreach (var slot in player.enemyItemInfo)
                        {
                            Debug.Log("Enemy has "+ slot.quantity + ' ' + slot.item.itemName);
                        }
                    }
                    Debug.Log("Player's turn to use skills");
                }
                
                
                break;

            case battleState.UseSkills:
                if (playerTurn && curSkillNum != maxSkillNum)
                {
                    Dictionary<KeyCode, int> skillKeyMap = new Dictionary<KeyCode, int>()
                    {
                        { KeyCode.Alpha1, 0 },
                        { KeyCode.Alpha2, 1 },
                        { KeyCode.Alpha3, 2 },
                        { KeyCode.Alpha4, 3 },
                        /*{ KeyCode.Alpha5, 4 },
                        { KeyCode.Alpha6, 5 }, */
                    };

                    foreach (var entry in skillKeyMap)
                    {
                        if (Input.GetKeyDown(entry.Key))
                        {
                            playerSkills[entry.Value].Use();
                            player.usedSkills.Add(player.skills[entry.Value]);
                            curSkillNum++;
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.N))
                    {
                        player.UpdateSkillCoolDown();
                        curState = battleState.Decision; // move to next state
                        Debug.Log("Player's turn to make decision");
                    }
                }
                else if (playerTurn && curSkillNum == maxSkillNum)
                {
                    curState = battleState.Decision; // move to next state
                    Debug.Log ("Player's turn to make decision");
                }
                else
                {
                    enemy.UseSkill();
                    enemy.UpdateSkillCoolDown();
                    curState = battleState.Decision; // move to next state
                    Debug.Log("Enemy's turn to make decision");
                }
                break;
                

            case battleState.Decision:
                
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
                    if (doubleDrink)
                    {
                        curState = battleState.UseItems;
                        Debug.Log("Enemy gets to drink again!");
                        Debug.Log("Player's turn to use items");
                        doubleDrink = false;
                    }
                    else
                    {
                        curState = battleState.Result;
                    }
                }
                    break;

            case battleState.Result:

                Result();

                
               
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
                if (reverse)
                {
                    usedItems.Clear(); //clear the used items since they won't be applied to enemy
                    player.lastConsent = true;
                    Debug.Log("Reverse effect activated! Enemy must drink instead of player.");
                    CalculateDamageResult();
                    return;
                }
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
                else
                {
                    player.lastConsent = false;
                    Debug.Log("Player chooses not to drink");
                }
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

                usedItems.Sort((a, b) => a.item.priority.CompareTo(b.item.priority)); // Sort items by priority (ascending order)
                foreach (InventorySlot slot in usedItems)
                {
                    slot.UseItem(enemy);
                }
                
            }
            enemy.ApplyDamage();
            player.ApplyDamage();
            Debug.Log("enemy's cap: " + enemy.cap);
            Debug.Log("player's cap: " + player.cap);
        }
        else
        {
            if (player.lastConsent)
            {
                if (reverse)
                {
                    enemy.pendingDamage += alcoholDmg;
                    reverse = false;
                }
                else player.pendingDamage += alcoholDmg;
            }
            player.ApplyDamage();
            enemy.ApplyDamage();
            Debug.Log("player's cap: " + player.cap);
            Debug.Log("enemy's cap: " + enemy.cap);
        }
        enemy.pendingDamage = 0f;
        player.pendingDamage = 0f;

        usedItems.Clear();

    }

    private void Result()
    {
        if (playerTurn && player.hasAdrenaline)
        {
            if (player.adrenalineTurn == 0)
            {
                Debug.Log("Adrenaline effect has worn off. Player lost!");
                
            }
            player.adrenalineTurn = playerTurn ? player.adrenalineTurn - 1 : player.adrenalineTurn;
        }
        else if (playerWin == 0)
        {
            Debug.Log("Player lost!");
        }
        else if (enemy.cap >= enemy.maxCap)
        {
            Debug.Log("Enemy is drunk! Player wins!");
            playerWin = 1;
        }
        else if (player.cap >= player.maxCap)
        {
            Debug.Log("Player is drunk! Enemy wins!");
            playerWin = 0;
        }
        if (playerWin != -1)
        {
            playerWin = -1;
        }

        playerTurn = !playerTurn; // switch turn
        curState = battleState.RollDice;

    }
    



    
}
