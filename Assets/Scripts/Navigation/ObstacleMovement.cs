using DG.Tweening;
using UnityEngine;



public class ObstacleMovement : MonoBehaviour {
    [SerializeField] private Vector3 MoveVector;
    [SerializeField] private float MoveDuration = 3f;


    private void Start(){
        MoveObstacle();
    }

    private void MoveObstacle(){
        Vector3 originPos = transform.position;
        Vector3 destinationPos = transform.position + MoveVector;

        transform.DOMove(destinationPos, MoveDuration).SetEase(Ease.InOutCubic).OnComplete(() => {
            transform.DOMove(originPos, MoveDuration)
                .SetEase(Ease.InOutCubic)
                .SetLoops(-1, LoopType.Yoyo);
        });
    }
}