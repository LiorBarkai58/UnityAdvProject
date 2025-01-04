using UnityEngine;
using UnityEngine.Events;


public class Dart : MonoBehaviour {

    [SerializeField] private DartData dartData;

    public DartData DartData {get {return dartData;}}

    private Vector3 direction = new Vector3(0, 0, 1);

    public event UnityAction<DartHitArgs> OnDartHitAction;


    private void OnEnable(){
        Destroy(gameObject, 5f);
    }
    private void FixedUpdate(){
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, dartData.DartSpeed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            OnDartHitAction.Invoke(new DartHitArgs{dartHit = this,Damage = dartData.DartDamage, playerHit = other.GetComponent<PlayerCharacter>()});
        }
    }
    public void SetDirection(Vector3 direction){
        this.direction = direction;
    }
}

public struct DartHitArgs {
    public Dart dartHit;

    public float Damage;

    public PlayerCharacter playerHit;


}