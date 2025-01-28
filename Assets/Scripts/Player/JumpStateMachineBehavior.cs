using UnityEngine;



public class JumpStateMachineBehavior : StateMachineBehaviour {
    private PlayerController playerController;


    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineEnter(animator, stateMachinePathHash);
        base.OnStateMachineExit(animator, stateMachinePathHash);
        if(playerController == null){
            playerController = animator.gameObject.GetComponentInParent<PlayerController>();
        }
        if(playerController != null){
            playerController.OnJumpStart();
        }
        else{
            Debug.Log("Player Controller not found");
        }
    }
    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineExit(animator, stateMachinePathHash);
        if(playerController == null){
            playerController = animator.gameObject.GetComponentInParent<PlayerController>();
        }
        if(playerController != null){
            playerController.OnJumpEnd();
        }
        else{
            Debug.Log("Player Controller not found");
        }
    }
}