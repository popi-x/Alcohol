using UnityEngine;

public class ShakerBinder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator anim;
    public GameObject shaker;

    void Start()
    {
        anim = GetComponent<Animator>();
        var behaviors = anim.GetBehaviours<ShakerController>();    
        foreach (var behavior in behaviors)
        {
            behavior.shaker = shaker;
        }
    }

}
