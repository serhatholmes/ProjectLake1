using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyKontrol : MonoBehaviour
{

    public Animator Animator {  get { return mAnimator; } }

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
        if(mNavMeshAgent.enabled)
        {
            mNavMeshAgent.speed = (mAnimator.deltaPosition / Time.fixedDeltaTime).magnitude * mSpeedModifier;
        }

       
    }

    public bool FollowTarget(Vector3 position)
    {
        if(!mNavMeshAgent.enabled)
        {
            mNavMeshAgent.enabled = true;
        }

        return mNavMeshAgent.SetDestination(position);
    }

    public void StopFollowTarget()
    {


        mNavMeshAgent.enabled = false;
    }

}
