using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   [Header("References")]
   [SerializeField] private NavMeshAgent agent;
   [SerializeField] private LayerMask groundLayer;//Layer for the raycast that controls the movement
   [SerializeField] private Animator animator;

   [Header("Player Data")]

   [SerializeField] private float CastingSpeedMultiplier = 1;//Controls the cast animation speed, use context menu to update the animator

   [SerializeField] private float CastCooldown = 3;//Cooldown until you can properly cast again

   [SerializeField] private float MaxHP = 20;

    [SerializeField] public UnityEvent playerTakeDamageEvent = new UnityEvent() ;

    private float currentHP = 0;


   [Header("Agent Data")]
    [SerializeField] private Areas areaResistence = Areas.Walkable;//Area the player is resistent to
    [SerializeField] private Areas preferredArea = Areas.Grass;//Area the player prefers to walk on

    


   private static readonly int SpeedHash = Animator.StringToHash("Speed");
   private static readonly int CastingSpeed = Animator.StringToHash("CastingSpeed");
   private static readonly int CastHash = Animator.StringToHash("Cast");
   private static readonly int OnLinkHash = Animator.StringToHash("OnLink");

   private bool _castOnCooldown = false;


   private bool _isJumping = false;
   void Start(){
      //Sets casting speed, which controls the speed of the casting animation
      animator.SetFloat(CastingSpeed, CastingSpeedMultiplier);


      //Sets cost of specific resisted area to 1
      agent.SetAreaCost((int)areaResistence, 1);
      agent.SetAreaCost((int)preferredArea, 1);
      currentHP = MaxHP;
   }
   void Update(){
      //Sets the current speed variable for the animatori
      animator.SetFloat(SpeedHash, agent.velocity.magnitude/agent.speed);

      //Checks if the player is on a link and is not in a jumping animation, to initiate a jump
      if(agent.isOnOffMeshLink && !_isJumping){
         animator.SetBool(OnLinkHash, true);
      }
      //Checks if the player was jumping and is no longer on a navmeshlink, to stop the jump animation
      if(_isJumping && !agent.isOnOffMeshLink){
         animator.SetBool(OnLinkHash,false);

      }

   }

   public void OnMouseInput(InputAction.CallbackContext context){
      if(context.started){//When mouse was clicked
         Vector3 mousePosition = Mouse.current.position.ReadValue();//Read mouse position

         // Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
         Ray ray = Camera.main.ScreenPointToRay(mousePosition);//Create a ray
         RaycastHit hit;
         if(Physics.Raycast(ray, out hit, 100, groundLayer)){//Send a ray to ground
               agent.SetDestination(hit.point);//send agent to position clicked
         }
      }
   }

   public void OnCast(InputAction.CallbackContext context){//Play cast animation(no actual spell right now just the animation)
         //important to note, the ability to cast again even when on cooldown is deliberate
         //It is meant to play a "lesser" animation to make the player understand the cast is on cooldown so it doesn't have the full effect
        if(context.started){
            animator.SetTrigger(CastHash);
        }
     }

   

   [ContextMenu("Update casting")]
   public void UpdateCastingSpeed(){//Context menu function to update the casting speed parameter of the cast from the serialized field variable
      animator.SetFloat(CastingSpeed, CastingSpeedMultiplier);
      
   }

   private IEnumerator StartCastCooldown(){//Starts cast cooldown when cast is used and is not on cooldown
      _castOnCooldown = true;
      yield return new WaitForSeconds(CastCooldown);
      _castOnCooldown = false;
   }


   public void OnCastStart(){//When cast starts, set casting to true called from statemachine behavior
      if(!_castOnCooldown){//if the cast is not on cooldown, it will set the weight for the proper animation of the upper body with weight 1 and start counting the cooldown
         animator.SetLayerWeight(1, 1f);
         StartCoroutine(StartCastCooldown());
      }
      else{//if the cast is on cooldown it will set weight to 0.4 to display a much lesser version of the animation, to help the player understand the cast is on cooldown
         animator.SetLayerWeight(1, 0.4f); 
      }
   }

   public void OnJumpStart(){//Called from statemachinebehavior, when jump animation starts to allow the code to know when the jump is still underway
      _isJumping = true;
   }
   public void OnJumpEnd(){//Called from statemachinebehavior, when jump animation ends to allow the code to know when the jump animation is done
      _isJumping = false;
   }

   public void TakeDamage(float Damage){//Simple take damage function
        currentHP -= Damage;
        playerTakeDamageEvent.Invoke();
    }

     
}
