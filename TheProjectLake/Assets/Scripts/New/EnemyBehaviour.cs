using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace SoulsLike
{
    public class EnemyBehaviour : MonoBehaviour
    {

        public PlayerScanner playerScanner;

        public float timeToStopPursuit = 2.0f;
        public float timeToWaitOnPursuit = 2.0f;
        public float attackDistance = 1.3f;

        public bool HasFollowingTarget
        {
            get
            {
                return mFollowTarget != null;
            }
        } 

        private PlayerKontrol mFollowTarget;
        private EnemyKontrol mEnemyKontrol;

        //private NavMeshAgent mNavMeshAgent;
        private float mTimeSinceLostTarget = 0;
        private Vector3 mOriginalPosition;
        private Quaternion mOriginalRotation;
        private Animator mAnimator;

        private readonly int HashInPursuit = Animator.StringToHash("InPursuit");
        private readonly int HashNearBase = Animator.StringToHash("NearBase");
        private readonly int HashAttack = Animator.StringToHash("Attack");

        private void Awake()
        {
            mEnemyKontrol = GetComponent<EnemyKontrol>();
            mOriginalPosition = transform.position;
            mAnimator = GetComponent<Animator>();
            mOriginalRotation = transform.rotation;
        }
        private void Update()
        {
            //Debug.Log(PlayerKontrol.Instance);
            //LookForPlayer();
            var detectedTarget = playerScanner.Detect(transform);
            bool hasDetectedTarget = detectedTarget != null;

            if(hasDetectedTarget)
            {
                mFollowTarget = detectedTarget;
            }

            if(HasFollowingTarget)
            {
                AttackFollowTarget();

                if(hasDetectedTarget)
                {
                    mTimeSinceLostTarget = 0;
                }
                else
                {
                    StopPursuit();
                }
            }

            CheckIfNearBase();

        }

        private void AttackFollowTarget()
        {
            Vector3 toTarget = mFollowTarget.transform.position - transform.position;
            if (toTarget.magnitude <= attackDistance)
            {
                //Debug.Log("Attack!");
                mEnemyKontrol.StopFollowTarget();


                mAnimator.SetTrigger(HashAttack);
            }
            else
            {
                mAnimator.SetBool(HashInPursuit, true);
                mEnemyKontrol.FollowTarget(mFollowTarget.transform.position);

            }
        }

        private void StopPursuit()
        {
            mTimeSinceLostTarget += Time.deltaTime;
            if (mTimeSinceLostTarget >= timeToStopPursuit)
            {
                mFollowTarget = null;
                //Debug.Log("stopping the enemy!");
                //mNavMeshAgent.isStopped = true;
                mAnimator.SetBool("InPursuit", false);
                StartCoroutine(WaitOnPursuit());
            }
        }

        private void CheckIfNearBase()
        {
            Vector3 toBase = mOriginalPosition - transform.position;
            toBase.y = 0;

            bool nearBase = toBase.magnitude < 0.01f;

            mAnimator.SetBool(HashNearBase, nearBase);

            if (nearBase)
            {
                Quaternion targetRotation = Quaternion.RotateTowards(transform.rotation, mOriginalRotation, 360 * Time.deltaTime);

                transform.rotation = targetRotation;
            }
        }

        private IEnumerator WaitOnPursuit()
        {
            yield return new WaitForSeconds(timeToWaitOnPursuit);
            //mNavMeshAgent.isStopped = false;
            mEnemyKontrol.FollowTarget(mOriginalPosition);
        }

      


#if UNITY_EDITOR
        // gizmoda karakter seçiliyken çalýþýr
        private void OnDrawGizmosSelected()
        {
            // düþmanýn görüþ açýsýný görselleþtirme
            Color c = new Color(0.8f, 0, 0, 0.4f);
            UnityEditor.Handles.color = c;

            Vector3 rotatedForward = Quaternion.Euler(0,-playerScanner.detectionAngle * 0.5f,0) * transform.forward;

            UnityEditor.Handles.DrawSolidArc(transform.position,Vector3.up,rotatedForward,playerScanner.detectionAngle, playerScanner.detectionRadius);

        }
#endif

    }

}

