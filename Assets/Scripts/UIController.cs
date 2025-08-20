using UnityEngine;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI fb; 
    public TextMeshProUGUI diceRoll;
    public GameObject rollDiceBtn;
    public static UIController instance;

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

    public void UpdateAmount(int amountValue)
    {
        fb.text = "Amount: " + amountValue.ToString();
        Debug.Log("UI Updated: Amount is now " + amountValue);
    }

    public void UpdateDiceRoll(int n)
    {
        diceRoll.text = "Enemy rolled: " + n.ToString();
    }

    public void UpdateFB(string str)
    {
       fb.text = str; 
    }

    public void ShowItemSets()
    {
        rollDiceBtn.SetActive(false);
    }
}
