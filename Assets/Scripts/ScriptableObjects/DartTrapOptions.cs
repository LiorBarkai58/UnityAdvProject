using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dart Trap Data", menuName = "Darts/Dart Trap Data")]
public class DartTrapOptions : ScriptableObject {
    [SerializeField] private List<Dart> dartPrefabs;

    public Dart GetRandomPrefab(){
        if(dartPrefabs.Count > 0){
            return dartPrefabs[Random.Range(0, dartPrefabs.Count)];
        }
        return null;
    }


}