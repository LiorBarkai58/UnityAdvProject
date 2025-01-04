using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector3> GoalSelected = new UnityEvent<Vector3>();

    public event UnityAction<Vector3> GoalSelectedAction;
    [SerializeField] private LayerMask groundLayer;

    
    
    public void OnMouseInput(InputAction.CallbackContext context){
        if(context.started){//When mouse was clicked
            Vector3 mousePosition = Mouse.current.position.ReadValue();//Read mouse position

            // Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);//Create a ray
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100, groundLayer)){//Send a ray to ground
                Debug.Log($"hit ground {hit.point}");
                GoalSelected.Invoke(hit.point);
                GoalSelectedAction?.Invoke(hit.point);
            }
        }
     }
}
