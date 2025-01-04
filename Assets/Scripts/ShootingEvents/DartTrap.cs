using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class DartTrap : MonoBehaviour {

    [SerializeField] private DartTrapOptions dartOptions;
    [SerializeField] private DartData dartPreference;
    [SerializeField] private ParticleSystem dartHitParticle;

    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform shootingTarget;

    public event UnityAction<PlayerHitData> onPlayerHit;


    private void Start(){
        StartCoroutine(ShootingCycle());
    }

    private IEnumerator ShootingCycle(){
        while(true){
            Dart currentDart = Instantiate(dartOptions.GetRandomPrefab(dartPreference), shootingPoint.position, Quaternion.identity);
            currentDart.SetDirection((shootingTarget.position - shootingPoint.position).normalized);
            currentDart.OnDartHitAction += OnDartHit;
            
            yield return new WaitForSeconds(5);
        }
    }

    private void OnDartHit(DartHitArgs dartHitArgs){
        ParticleSystem currentHitEffect = Instantiate(dartHitParticle, dartHitArgs.dartHit.transform.position,Quaternion.identity);
        currentHitEffect.Play();
        onPlayerHit?.Invoke(new PlayerHitData{PlayerHit = dartHitArgs.playerHit, Damage = dartHitArgs.Damage});
        dartHitArgs.dartHit.OnDartHitAction -= OnDartHit;//Wondering if this is needed, or this is taken care of internally by the system once the gameobject associated with the subscribed function is destroyed
        Destroy(dartHitArgs.dartHit.gameObject);
    }
}