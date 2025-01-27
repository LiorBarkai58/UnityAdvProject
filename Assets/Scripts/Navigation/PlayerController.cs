using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private Animator animator;

    [SerializeField] private float Speed = 6;

    [SerializeField] private float Acceleration = 6;

    [SerializeField] private float CastingSpeedMultiplier = 1;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int CastingSpeed = Animator.StringToHash("CastingSpeed");
    private static readonly int Cast = Animator.StringToHash("Cast");
    private static readonly int Jump = Animator.StringToHash("Jump");


    private Vector3 _movementDirection;
    private Vector2 _movementInput;

    private Vector2 _lookInput;

    private Vector3 _lookDirection;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        animator.SetFloat(CastingSpeed, CastingSpeedMultiplier);
    }
    void Update(){
        _movementDirection = transform.forward * _movementInput.y + transform.right * _movementInput.x;
        _movementDirection.y = 0;
        transform.Rotate(_lookDirection * 10 * Time.deltaTime);

    }
    void FixedUpdate(){
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, _movementDirection * Speed, Acceleration*Time.deltaTime);
        animator.SetFloat(SpeedHash, rb.linearVelocity.magnitude/Speed);//current velocity/maxvelocity

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

     
}
