using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    [SerializeField] private UnityEvent<string> AgentArrived = new UnityEvent<string>();


    public event UnityAction<string> AgentArrivedAction;
    private bool _goalReached = false;

    
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("Player reached goal");
            if(!_goalReached){
                AgentArrived?.Invoke(other.gameObject.name);
                AgentArrivedAction?.Invoke(other.gameObject.name);
                _goalReached = true;
            }
        }
    }
}
