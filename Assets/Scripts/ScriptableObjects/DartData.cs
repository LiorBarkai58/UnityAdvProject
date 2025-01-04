using UnityEngine;

[CreateAssetMenu(fileName = "Dart Data", menuName = "Darts")]
public class DartData : ScriptableObject {
    [SerializeField] private float dartSpeed;

    public float DartSpeed {get {return dartSpeed;}}

    [SerializeField] private float dartDamage;

    public float DartDamage {get {return dartDamage;}}


}