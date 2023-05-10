using UnityEngine;
using UnityEngine.AI;

[SelectionBase]    // (If you click any child objects in the inspector, it will automatically select the parent object which contains this script
[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(ElementComponent))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    // Enemy properties (public so the state classes can access them)
    [HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent navAgent;
    [HideInInspector] public Transform playerTransform;
    [HideInInspector] public ElementComponent elementComponent;
    [HideInInspector] public EnemySpawnManager spawnManager;

    public float closeAttackDistance = 2f;
    public float farAttackDistance = 4f;
    [HideInInspector] public float midAttackDistance;
    public float hitDamage = 10f;
    public float enemyTurnSpeed = 5f;
    public float facingPlayerDegreesMargin = 10f;   // The plus/minus margin of error (in degrees) that the enemy can be facing to the left or right of the player but still be close enough to be considered 'facing the player'

    public float hitDetectDistance = 1f;
    public Vector3 hitDetectBoxSize = new Vector3(0.5f, 0.5f, 0.5f);
    public LayerMask playerLayerMask;
    PlayerHealthHandler playerHealthHandler;
    Color debugColor = Color.red;

    // State machine properties
    public EnemyBaseState currentState { get; private set; }
    public EnemyIdleState IdleState = new EnemyIdleState();    // These 'potential' states need to be public so that the concrete State classes can refer to them when telling the Controller which state to switch to
    public EnemyMoveState MoveState = new EnemyMoveState();
    public EnemyAttackState AttackState = new EnemyAttackState();
    public EnemyGotHitState GotHitState = new EnemyGotHitState();
    public EnemyDieState DieState = new EnemyDieState();


    private void Awake()
    {
        // NavMeshAgent
        navAgent = GetComponent<NavMeshAgent>();
        playerTransform = FindObjectOfType<PlayerHealthHandler>(true).transform;
        playerHealthHandler = playerTransform.GetComponent<PlayerHealthHandler>();
        navAgent.isStopped = false;

        animator = GetComponent<Animator>();
        elementComponent = GetComponent<ElementComponent>();

        midAttackDistance = closeAttackDistance + ((farAttackDistance - closeAttackDistance) / 2);

        SwitchState(IdleState);
    }


    public void SetElement(Element.Type newElement)
    {
        elementComponent.SwitchType(newElement);
        // PLUS: add in code to update the colour of the crystal shards on the enemy
    }


    private void Update()
    {
        // Triggers the UpdateState function each frame on whichever state is current
        currentState.UpdateState(this);
    }


    public void SwitchState(EnemyBaseState newState)
    {
        // This is public because it will be called from whichever concrete State class is the 'currentState'.
        // currentState will pass in a reference to one of this EnemyController's other public state properties (e.g. MoveState or AttackState)
        currentState = newState;
        currentState.EnterState(this);
    }


    public void AnimationFinished(string animName)
    {
        // This gets called by an AnimationEvent on relevant AnimationClips (e.g. after an attack animation has completed)
        // This should be fed to the currentState to see whether the state needs to respond to the animation finishing
        currentState.AnimationClipFinished(this, animName);
    }


    public float DistanceToPlayer()
    {
        float distance = (playerTransform.position - this.transform.position).magnitude;
        //Debug.Log("Distance to player: " + distance.ToString());
        return distance;
    }


    public Vector3 FlatDirectionToPlayer()
    {
        // Returns the direction of the player on a flat plane i.e. with no up/down element
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        Vector3 flatDirectionToPlayer = new Vector3(directionToPlayer.x, 0f, directionToPlayer.z);
        return flatDirectionToPlayer;

    }

    public Vector3 FlatEnemyFacingDirection()
    {
        Vector3 flatDirection = new Vector3(transform.forward.x, 0f, transform.forward.z);
        return flatDirection;
    }


    public bool EnemyIsFacingPlayer()
    {
        float angle = Vector3.Angle(FlatDirectionToPlayer(), FlatEnemyFacingDirection());
        bool isFacing = angle < facingPlayerDegreesMargin;
        return isFacing;
    }


    public void TurnTowardsPlayer()
    {
        Quaternion rotationToPlayer = Quaternion.LookRotation(FlatDirectionToPlayer());
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToPlayer, enemyTurnSpeed * Time.deltaTime);
        
    }


    public Vector3 PositionToMoveTo(float distanceFromPlayer)
    {
        Vector3 playerToEnemyDirection = (transform.position - playerTransform.position).normalized;
        Vector3 targetPosition = playerTransform.position + (playerToEnemyDirection * distanceFromPlayer);
        return targetPosition;
    }


    
    public void TestIfEnemyHitPlayer()
    {
        // This is called by an animation event in the two attack animations
        // Cast a box-shaped cast from the character's position forward
        RaycastHit hit;
        if (Physics.BoxCast(transform.position, hitDetectBoxSize, transform.forward, out hit, transform.rotation, hitDetectDistance, playerLayerMask))
        {
            // Check if the hit object matches the object we're testing for
            if (hit.collider.transform == playerTransform)
            {
                // The object is in the defined space
                playerHealthHandler.DamagePlayer(hitDamage);
                //Debug.Log("Player hit!  Player health = " + playerHealthHandler.Health);
            } else
            {
                //Debug.Log("TestIfEnemyHitPlayer() has detected something, but not the player: " + hit.collider.gameObject.name);
            }
        } else
        {
            //Debug.Log("TestIfEnemyHitPlayer() has run but not detected the player.");
        }

        // Visualize the BoxCast in the Scene view
        Debug.DrawRay(transform.position, transform.forward * hitDetectDistance, debugColor);
        Debug.DrawRay(transform.position + transform.right * hitDetectBoxSize.x, transform.forward * hitDetectDistance, debugColor);
        Debug.DrawRay(transform.position - transform.right * hitDetectBoxSize.x, transform.forward * hitDetectDistance, debugColor);
        Debug.DrawRay(transform.position + transform.up * hitDetectBoxSize.y, transform.forward * hitDetectDistance, debugColor);
        Debug.DrawRay(transform.position - transform.up * hitDetectBoxSize.y, transform.forward * hitDetectDistance, debugColor);
        Debug.DrawRay(transform.position + transform.forward * hitDetectBoxSize.z, transform.right * hitDetectBoxSize.x * 2, debugColor);
        Debug.DrawRay(transform.position - transform.forward * hitDetectBoxSize.z, transform.right * hitDetectBoxSize.x * 2, debugColor);
        Debug.DrawRay(transform.position + transform.forward * hitDetectBoxSize.z, transform.up * hitDetectBoxSize.y * 2, debugColor);
        Debug.DrawRay(transform.position - transform.forward * hitDetectBoxSize.z, transform.up * hitDetectBoxSize.y * 2, debugColor);
    }


    public void LaserContactBegins()
    {
        animator.SetBool("WalkForwards", false);
        navAgent.isStopped = true;
        SwitchState(GotHitState);
    }


    public void EnemyHasDied()
    {
        SwitchState(DieState);
    }



    [ContextMenu("Kill this enemy (debug)")]
    private void Debug_KillEnemy()
    {
        SwitchState(DieState);
    }
}
