using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectDawn.Demo
{
    public class SetTrigger : StateMachineBehaviour
    {
        public string TriggerName;
        public float TriggerTime;
        bool m_Active;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_Active = true;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (m_Active && stateInfo.normalizedTime > TriggerTime)
            {
                animator.SetBool(TriggerName, true);
                m_Active = false;
            }
        }
    }
}
