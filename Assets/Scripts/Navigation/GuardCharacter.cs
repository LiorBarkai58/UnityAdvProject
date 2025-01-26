using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum Areas {
    Walkable,
    NotWalkable,
    Jump,
    Fire,
    Mud,
    Grass,
    Gravel
}
public class GuardCharacter : MonoBehaviour {
    [Header("References")]
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private PlayerController playerControls;

    [SerializeField] private Animator animator;

    [Header("Agent Data")]
    [SerializeField] private Areas areaResistence = Areas.Walkable;
    [SerializeField] private Areas preferredArea = Areas.Grass;

    [SerializeField] private float MaxHP = 20;

    [SerializeField] public UnityEvent playerTakeDamageEvent = new UnityEvent() ;

    private float currentHP = 0;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    private static readonly int JumpingHash = Animator.StringToHash("Jumping");

    private void OnEnable(){
        currentHP = MaxHP;
    }
    
    private void Start(){
        playerControls.GoalSelectedAction += SetDestination;

        //Sets cost of specific resisted area to 1
        agent.SetAreaCost((int)areaResistence, 1);
        agent.SetAreaCost((int)preferredArea, 1);
    }

    private void Update(){
        if(animator != null){
            animator.SetFloat(SpeedHash, agent.velocity.magnitude/agent.speed);
        }
        if(agent.isOnOffMeshLink){
            animator.SetBool(JumpingHash, true);
        }
        else{
            animator.SetBool(JumpingHash, false);
        }
    }

    private void SetDestination(Vector3 destination){
        agent.SetDestination(destination);
    }

    public void TakeDamage(float Damage){
        currentHP -= Damage;
        playerTakeDamageEvent.Invoke();
    }
}