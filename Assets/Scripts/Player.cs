using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }
    public int diceRoll;
    public int adrenalineTurn = 2;
    public float cap = 0f;
    public float maxCap = 5f;
    public int money = 0;
    public Inventory inventory = new Inventory(10);
    public float pendingDamage = 0f;
    public List<InventorySlot> enemyItemInfo;  
    public List<SkillRuntime> skills = new List<SkillRuntime>();
    public List<SkillRuntime> usedSkills = new List<SkillRuntime>();

    public bool lastConsent { get; set; } = true;
    public bool hasAdrenaline = false;

    private WalkController walkController;
    private DialogueController dialogueController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        var src = Resources.LoadAll<PlayerItem>("Items");
        foreach (var item in src)
        {
            inventory.AddItem(item, 2);
        }

        var skillSrc = Resources.LoadAll<BaseSkill>("Skills/Player");
        foreach (var skill in skillSrc)
        {
            skills.Add(new SkillRuntime(skill));
        }

        walkController = GetComponent<WalkController>();
        dialogueController = GetComponent<DialogueController>();
    }

    private void Update()
    {
        
    }

    public void ResetBattleState()
    {
        adrenalineTurn = 0;
        hasAdrenaline = false;
        pendingDamage = 0f;
    }



    public void ApplyDamage()
    {
        cap += pendingDamage;
    }

    public void UpdateSkillCoolDown()
    {
        foreach (var skill in skills)
        {
           skill.UpdateCoolDown(); 
        }
    }
}
