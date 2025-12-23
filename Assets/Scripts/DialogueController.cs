using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private InputAction dialogueAction;
    public static DialogueController instance { get; private set; }

    public bool talkEnabled = false;

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
        
    }

    private void OnEnable()
    {
        dialogueAction.Enable();
        dialogueAction.performed += OnDialogue;
    }

    private void OnDisable()
    {
        dialogueAction.Disable();
        dialogueAction.performed -= OnDialogue;

    }

    public void OnDialogue(InputAction.CallbackContext context)
    {
        if (talkEnabled)
        {
            Debug.Log("Dialogue Triggered");
            GameManager.instance.curState = GameManager.GameState.Battle; 
            
        }
        else
        {
            Debug.Log("Dialogue not enabled");
            GameManager.instance.curState = GameManager.GameState.Idle;
        }
    }
}
