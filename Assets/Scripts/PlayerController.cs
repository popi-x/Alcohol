using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public Camera cam;
    public static event System.Action<Vector3> OnGroundTouch;

    private Animator animator;
    private Vector3 inputDir;
    private NavMeshAgent agent;
    private Coroutine coroutine;

    [SerializeField] private InputAction mouseClickAction;
    [SerializeField] private LayerMask groundLayer;  
    [SerializeField] private float sampleDistance = 0.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

    }

    private void OnEnable()
    {
        mouseClickAction.Enable();
        mouseClickAction.performed += Move;
    }

    private void OnDisable()
    {
        mouseClickAction.performed -= Move;
        mouseClickAction.Disable();
    }

    public void Move(InputAction.CallbackContext context)
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, groundLayer))
        {
            Debug.Log("Hit point: " + hit.point);
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navMeshHit, sampleDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(navMeshHit.position);
                OnGroundTouch?.Invoke(navMeshHit.position);
            }
        }
    }

    private void Update()
    {
        float normalizedSpeed = Mathf.InverseLerp(0, agent.speed, agent.velocity.magnitude);
        animator.SetFloat("Speed", normalizedSpeed);
    }


}
