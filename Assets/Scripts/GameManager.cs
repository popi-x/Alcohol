using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        Idle,
        PreDialogue,
        Battle,
        PostDialogue,
        Shop
    }
    public GameState curState { get; set; } = GameState.Idle;
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

    public void Update()
    {
        
    }


}
