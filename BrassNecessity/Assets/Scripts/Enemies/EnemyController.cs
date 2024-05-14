using UnityEngine;
using UnityEngine.AI;
using EnemyStateMachine;

[SelectionBase]    // (If you click any child objects in the inspector, it will automatically select the parent object which contains this script
[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(ElementComponent))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    // Enemy properties (public so the state classes can access them)
    private Animator animator;
    private NavMeshAgent navAgent;
    [HideInInspector] public EnemySpawnManager spawnManager;
    private ElementComponent elementComponent;
    private Transform playerTransform;

    [SerializeField]
    private float closeAttackDistance = 2f;
    [SerializeField] 
    private float farAttackDistance = 4f;
    [HideInInspector] public float midAttackDistance;
    [SerializeField]
    private float hitDamage = 10f;
    [SerializeField]
    private float enemyTurnSpeed = 5f;
    [SerializeField] 
    private float facingPlayerDegreesMargin = 10f;   // The plus/minus margin of error (in degrees) that the enemy can be facing to the left or right of the player but still be close enough to be considered 'facing the player'

    [SerializeField]
    private float hitDetectDistance = 1f;
    [SerializeField]
    private Vector3 hitDetectBoxSize = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField]
    private LayerMask playerLayerMask;
    private PlayerHealthHandler playerHealthHandler;
    Color debugColor = Color.red;

    // State machine properties
    [SerializeField]
    private StateMachine enemyState;

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
        setupStateMachine();
    }

    private void setupStateMachine()
    {
        EnemyContext enemyContext = new EnemyContext
        {
            Animator = this.animator,
            NavAgent = this.navAgent,
            SpawnManager = this.spawnManager,
            FarAttackDistance = this.farAttackDistance,
            MidAttackDistance = this.midAttackDistance,
            EnemyObject = this.gameObject,
            Controller = this
        };
        enemyState.InitialiseStateMachine(enemyContext);
    }

    public void AddEnemySpawnerParent(EnemySpawnManager spawnManager)
    {
        this.spawnManager = spawnManager;
        enemyState.AddSpawnManager(spawnManager);
    }


    public void SetElement(ElementPair newElement)
    {
        elementComponent.SwitchType(newElement.Primary, newElement.Secondary);
        // PLUS: add in code to update the colour of the crystal shards on the enemy
    }


    private void Update()
    {
        enemyState.UpdateCurrentState();
    }


    public void SwitchState(AbstractState newState)
    {
        // This is public because it will be called from whichever concrete State class is the 'currentState'.
        // currentState will pass in a reference to one of this EnemyController's other public state properties (e.g. MoveState or AttackState)
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
            }
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
        enemyState.AttemptStateUpdate(StateType.Hit);
    }


    public void EnemyHasDied()
    {
        enemyState.AttemptStateUpdate(StateType.Death);
    }



    [ContextMenu("Kill this enemy (debug)")]
    private void Debug_KillEnemy()
    {
        enemyState.AttemptStateUpdate(StateType.Death);
    }
}
