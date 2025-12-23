using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Enemy npc;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered dialogue range.");
           DialogueController.instance.talkEnabled = true;
            BattleManager.instance.enemy = npc;                                                                                                                                                                                                                               
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited dialogue range.");
           DialogueController.instance.talkEnabled = false;
        }
    }
}
