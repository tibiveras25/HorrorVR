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

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (player == null || agent == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        HandleMovement(distance);
        HandleAnimations(distance);
    }

    void HandleMovement(float distance)
    {
        if (distance <= jumpscareDistance)
        {
            agent.isStopped = true;

            // Make the enemy face the player during the jumpscare
            Vector3 lookPos = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(lookPos);

            if (!hasJumpScared)
            {
                hasJumpScared = true;
                animator.SetTrigger("JumpScare");
            }
        }
        else if (distance < chaseDistance)
        {
            hasJumpScared = false;
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;
        }
    }

    void HandleAnimations(float distance)
    {
        if (animator == null) return;

        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);

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

    private void SetAnimationStates(bool idle, bool walking, bool running, bool attacking)
    {
        animator.SetBool("IsIdle", idle);
        animator.SetBool("IsWalking", walking);
        animator.SetBool("IsRunning", running);
        animator.SetBool("IsAttacking", attacking);
    }
}