using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyKontrol : MonoBehaviour
{

    private NavMeshAgent mNavMeshAgent;
    private Animator mAnimator;
    private float mSpeedModifier = 0.6f;

    private void Awake()
    {
        mAnimator = GetComponent<Animator>();
        mNavMeshAgent = GetComponent<NavMeshAgent>();
    }


    private void OnAnimatorMove()
    {
        if(mNavMeshAgent == null)
        {
            return;
        }

        mNavMeshAgent.speed = (mAnimator.deltaPosition / Time.fixedDeltaTime).magnitude * mSpeedModifier;
    }

    public bool SetFollowTarget(Vector3 position)
    {
        return mNavMeshAgent.SetDestination(position);
    }

}
