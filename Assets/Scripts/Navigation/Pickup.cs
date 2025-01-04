using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class Pickup : MonoBehaviour {

    [SerializeField] public UnityEvent PlayerPickedUpEvent;
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("Yippie");
            PlayerPickedUpEvent.Invoke();
        }
    }
}