using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerManager : MonoBehaviour {
    
    [SerializeField] private List<DartTrap> dartTraps;

    void Start(){
        foreach(DartTrap dartTrap in dartTraps){
            dartTrap.onPlayerHit += OnPlayerHit; 
        }
    }

    private void OnPlayerHit(PlayerHitData playerHitData){
        playerHitData.PlayerHit.TakeDamage(playerHitData.Damage);
        Debug.Log("Player took damage");
    }
}

public class PlayerHitData {
    public PlayerCharacter PlayerHit;

    public float Damage;


}