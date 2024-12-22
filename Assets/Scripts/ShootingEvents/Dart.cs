using UnityEngine;
using UnityEngine.Events;


public class Dart : MonoBehaviour {

    [SerializeField] private float DartSpeed = 5;

    private Vector3 direction = new Vector3(0, 0, 1);

    public event UnityAction<DartHitArgs> OnDartHitAction;

    private void OnEnable(){
        Destroy(gameObject, 5f);
    }
    private void FixedUpdate(){
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, DartSpeed * Time.fixedDeltaTime);
    }

    public void SetDirection(Vector3 direction){
        this.direction = direction;
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            OnDartHitAction.Invoke(new DartHitArgs{dartHit = this, agentHit = other.GetComponent<AgentMover>()});
        }
    }
}

public struct DartHitArgs {
    public Dart dartHit;

    public AgentMover agentHit;


}