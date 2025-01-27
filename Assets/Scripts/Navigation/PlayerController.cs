using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;

    private Vector3 _movementDirection;
    private Vector2 _movementInput;

    private Vector2 _lookInput;

    private Vector3 _lookDirection;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update(){
        _movementDirection = transform.forward * _movementInput.y + transform.right * _movementInput.x;
        _movementDirection.y = 0;
        characterController.Move(_movementDirection * 4 * Time.deltaTime);
        // transform.Rotate(_lookDirection * 10 * Time.deltaTime);

    }

     public void OnMovement(InputAction.CallbackContext context){
        _movementInput = context.ReadValue<Vector2>();
     }
     public void OnLook(InputAction.CallbackContext context){
        _lookInput = context.ReadValue<Vector2>();
        _lookDirection = new Vector3(0, _lookInput.x, 0);
     }
}
