using System.Collections;
using Unity.Cinemachine;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    [SerializeField] private Animator animator;

    [SerializeField] private CinemachineCamera FollowCam;

    [SerializeField] private float Speed = 6;

    [SerializeField] private float Acceleration = 6;

    [SerializeField] private float CastingSpeedMultiplier = 1;

    [SerializeField] private float CastCooldown = 3;


    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int CastingSpeed = Animator.StringToHash("CastingSpeed");
    private static readonly int Cast = Animator.StringToHash("Cast");
    private static readonly int Jump = Animator.StringToHash("Jump");


    private Vector3 _movementDirection;
    private Vector2 _movementInput;

    private Vector2 _lookInput;

    private Vector3 _lookDirection;

    private bool _castOnCooldown = false;

    private bool _isCasting = false;


    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        animator.SetFloat(CastingSpeed, CastingSpeedMultiplier);
    }
    void Update(){
        _movementDirection = transform.forward * _movementInput.y + transform.right * _movementInput.x;
        _movementDirection.y = 0;
        Vector3 newVelocity = Vector3.Lerp(characterController.velocity, _movementDirection * Speed, Acceleration * Time.deltaTime);
        characterController.Move(newVelocity*Time.deltaTime);
        animator.SetFloat(SpeedHash, characterController.velocity.magnitude/Speed);//current velocity/maxvelocity

          }
    void FixedUpdate(){
        

    }

     public void OnMovement(InputAction.CallbackContext context){
        _movementInput = context.ReadValue<Vector2>();
     }
     public void OnLook(InputAction.CallbackContext context){
        _lookInput = context.ReadValue<Vector2>();
        _lookDirection = new Vector3(0, _lookInput.x, 0);
     }

     public void OnAttack(InputAction.CallbackContext context){
        if(context.started){
            animator.SetTrigger(Cast);
            
        }
     }

     public void OnJump(InputAction.CallbackContext context){
        if(context.started){
            animator.SetTrigger(Jump);
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
        StartCoroutine(StartCastCooldown());
        _isCasting = false;
        if(!_castOnCooldown){
            animator.SetLayerWeight(1, 1f);
        }
        else{
            animator.SetLayerWeight(1, 0.4f); 
        }
     }

     public void OnCastStart(){
        _isCasting = true;
     }

     
}
