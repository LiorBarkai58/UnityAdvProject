using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    [HideInInspector] public UnityEvent<string> AgentArrived = new UnityEvent<string>();

    private bool _goalReached = false;
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("Player reached goal");
            if(!_goalReached){
                AgentArrived.Invoke(other.gameObject.name);
                _goalReached = true;
            }
        }
    }
}
