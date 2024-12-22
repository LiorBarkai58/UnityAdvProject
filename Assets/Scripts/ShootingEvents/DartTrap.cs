using System.Collections;
using UnityEngine;


public class DartTrap : MonoBehaviour {

    [SerializeField] private Dart dart;

    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform shootingTarget;

    private void Start(){
        StartCoroutine(ShootingCycle());
    }

    private IEnumerator ShootingCycle(){
        while(true){
            Dart currentDart = Instantiate(dart, shootingPoint.position, Quaternion.identity);
            currentDart.SetDirection((shootingTarget.position - shootingPoint.position).normalized);
            currentDart.OnDartHitAction += OnDartHit;
            yield return new WaitForSeconds(5);
        }
    }

    private void OnDartHit(DartHitArgs dartHitArgs){
        Destroy(dartHitArgs.dartHit.gameObject);
    }
}