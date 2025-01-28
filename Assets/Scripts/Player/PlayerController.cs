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
   [SerializeField] private LayerMask groundLayer;
   [SerializeField] private Animator animator;

   [Header("Player Data")]

   [SerializeField] private float CastingSpeedMultiplier = 1;

   [SerializeField] private float CastCooldown = 3;

   [SerializeField] private float MaxHP = 20;

    [SerializeField] public UnityEvent playerTakeDamageEvent = new UnityEvent() ;

    private float currentHP = 0;


   [Header("Agent Data")]
    [SerializeField] private Areas areaResistence = Areas.Walkable;
    [SerializeField] private Areas preferredArea = Areas.Grass;

    


   private static readonly int SpeedHash = Animator.StringToHash("Speed");
   private static readonly int CastingSpeed = Animator.StringToHash("CastingSpeed");
   private static readonly int CastHash = Animator.StringToHash("Cast");
   private static readonly int OnLinkHash = Animator.StringToHash("OnLink");

   private bool _castOnCooldown = false;

   private bool _isCasting = false;


   private bool _isJumping = false;
   void Start(){
      animator.SetFloat(CastingSpeed, CastingSpeedMultiplier);
      //Sets cost of specific resisted area to 1
      agent.SetAreaCost((int)areaResistence, 1);
      agent.SetAreaCost((int)preferredArea, 1);
      currentHP = MaxHP;
   }
   void Update(){
      animator.SetFloat(SpeedHash, agent.velocity.magnitude/agent.speed);

      if(agent.isOnOffMeshLink && !_isJumping){
         animator.SetBool(OnLinkHash, true);
      }
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
               Debug.Log($"hit ground {hit.point}");
               agent.SetDestination(hit.point);
         }
      }
   }

   public void OnAttack(InputAction.CallbackContext context){
        if(context.started){
            animator.SetTrigger(CastHash);
        }
     }

   public void OnJump(InputAction.CallbackContext context){
      if(context.started){
      }
   }

   [ContextMenu("Update casting")]
   public void UpdateCastingSpeed(){
      animator.SetFloat(CastingSpeed, CastingSpeedMultiplier);
      
   }

   private IEnumerator StartCastCooldown(){
      _castOnCooldown = true;
      yield return new WaitForSeconds(CastCooldown);
      _castOnCooldown = false;
      if(!_isCasting){
         animator.SetLayerWeight(1, 1);
      }
   }

   public void OnCastEnd(){
      _isCasting = false;
      
   }

   public void OnCastStart(){
      if(!_castOnCooldown){
         animator.SetLayerWeight(1, 1f);
         StartCoroutine(StartCastCooldown());
      }
      else{
         animator.SetLayerWeight(1, 0.4f); 
      }
      _isCasting = true;
      
   }

   public void OnJumpStart(){
      _isJumping = true;
   }
   public void OnJumpEnd(){
      _isJumping = false;
   }

   public void TakeDamage(float Damage){
        currentHP -= Damage;
        playerTakeDamageEvent.Invoke();
    }

     
}
