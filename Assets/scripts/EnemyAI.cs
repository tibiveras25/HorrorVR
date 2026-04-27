using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Animator animator;

    [Header("AI Settings")]
    public float chaseDistance = 10f;
    public float jumpscareDistance = 2f;
    public float walkSpeedThreshold = 0.1f;
    public float runSpeedThreshold = 3f;

    private NavMeshAgent agent;
    private bool hasJumpScared = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Fallback: If animator isn't assigned in inspector, try to find it on this object
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        // Safety check to avoid null reference errors
        if (player == null || agent == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        HandleMovement(distance);
        HandleAnimations(distance);
    }

    void HandleMovement(float distance)
    {
        if (distance <= jumpscareDistance)
        {
            // Stop moving when close enough to jumpscare
            agent.isStopped = true;

            if (!hasJumpScared)
            {
                hasJumpScared = true;
                animator.SetTrigger("JumpScare");
            }
        }
        else if (distance < chaseDistance)
        {
            // Reset jumpscare and start chasing
            hasJumpScared = false;
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            // Out of range: stop the agent
            agent.isStopped = true;
        }
    }

    void HandleAnimations(float distance)
    {
        if (animator == null) return;

        float speed = agent.velocity.magnitude;

        // Updates the float parameter if you're using a Blend Tree
        animator.SetFloat("Speed", speed);

        // State-based animation logic
        if (distance <= jumpscareDistance)
        {
            SetAnimationStates(idle: false, walking: false, running: false, attacking: true);
        }
        else if (speed <= walkSpeedThreshold)
        {
            SetAnimationStates(idle: true, walking: false, running: false, attacking: false);
        }
        else if (speed < runSpeedThreshold)
        {
            SetAnimationStates(idle: false, walking: true, running: false, attacking: false);
        }
        else
        {
            SetAnimationStates(idle: false, walking: false, running: true, attacking: false);
        }
    }

    // Helper method to keep HandleAnimations clean
    private void SetAnimationStates(bool idle, bool walking, bool running, bool attacking)
    {
        animator.SetBool("IsIdle", idle);
        animator.SetBool("IsWalking", walking);
        animator.SetBool("IsRunning", running);
        animator.SetBool("IsAttacking", attacking);
    }
}
