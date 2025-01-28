using UnityEngine;



public class CastStateMachine : StateMachineBehaviour {
    private PlayerController playerController;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if(playerController == null){
            playerController = animator.gameObject.GetComponentInParent<PlayerController>();
        }
        if(playerController != null){
            playerController.OnCastStart();
        }
        else{
            Debug.Log("Player Controller not found");
        }

    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        if(playerController == null){
            playerController = animator.gameObject.GetComponentInParent<PlayerController>();
        }
        if(playerController != null){
            playerController.OnCastEnd();
        }
        else{
            Debug.Log("Player Controller not found");
        }
    }
}