using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScript : StateMachineBehaviour
{
    private int swipeHash = Animator.StringToHash("Swipe");
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(swipeHash, false);
        
    }
}
