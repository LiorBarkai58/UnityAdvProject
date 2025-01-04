using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dart Trap Data", menuName = "Darts/Dart Trap Data")]
public class DartTrapOptions : ScriptableObject {
    [SerializeField] private List<Dart> dartPrefabs;

    public Dart GetRandomPrefab(DartData dartPreference){
        if(dartPreference != null){
            foreach(Dart dart in dartPrefabs){
                if(dart.DartData == dartPreference){
                    return dart;
                }
            }
        }
        if(dartPrefabs.Count > 0){
            return dartPrefabs[Random.Range(0, dartPrefabs.Count)];
        }
        return null;
    }


}