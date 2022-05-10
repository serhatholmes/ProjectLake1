using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



namespace SoulsLike
{
    public class EnemyBehaviour : MonoBehaviour, iMessageReceiver, IAttackAnimListener
    {

        public PlayerScanner playerScanner;
        public MeleeWeapons meleeWeapons;
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
        

        private readonly int HashInPursuit = Animator.StringToHash("InPursuit");
        private readonly int HashNearBase = Animator.StringToHash("NearBase");
        private readonly int HashAttack = Animator.StringToHash("Attack");
        private readonly int HashHurt = Animator.StringToHash("Hurt");
        private readonly int HashDead = Animator.StringToHash("Dead");

        private void Awake()
        {
            mEnemyKontrol = GetComponent<EnemyKontrol>();
            mOriginalPosition = transform.position;
           
            mOriginalRotation = transform.rotation;
            meleeWeapons.SetOwner(gameObject);
        }
        private void Update()
        {

            GuardPosition();
          

        }

        private void GuardPosition()
        {
            //Debug.Log(PlayerKontrol.Instance);
            //LookForPlayer();
            var detectedTarget = playerScanner.Detect(transform);
            bool hasDetectedTarget = detectedTarget != null;

            if (hasDetectedTarget)
            {
                mFollowTarget = detectedTarget;
            }

            if (HasFollowingTarget)
            {
                AttackFollowTarget();

                if (hasDetectedTarget)
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
                AttackTarget(toTarget);
            }
            else
            {
                FollowTarget();

            }
        }

        public void OnReceiveMessage(MessageType type,object sender, object msg)
        {
           switch(type)
            {
                case MessageType.DEAD:
                    OnDead();
                    break;
                case MessageType.DAMAGED:
                    OnReceiveDamage();
                    break;
                default:
                    break;

            }
        }

        public void MeleeAttackStart()
        {
            Debug.Log("Starting attack");
            meleeWeapons.AttackBegin();
        }

        public void MeleeAttackEnd()
        {
            Debug.Log("ending attack");
            meleeWeapons.AttackEnd();
        }

        private void OnDead()
        {
            mEnemyKontrol.StopFollowTarget();
            mEnemyKontrol.Animator.SetTrigger(HashDead);
        }

        private void OnReceiveDamage()
        {
            mEnemyKontrol.Animator.SetTrigger(HashHurt);
        }

        private void StopPursuit()
        {
            mTimeSinceLostTarget += Time.deltaTime;
            if (mTimeSinceLostTarget >= timeToStopPursuit)
            {
                mFollowTarget = null;
                //Debug.Log("stopping the enemy!");
                //mNavMeshAgent.isStopped = true;
                mEnemyKontrol.Animator.SetBool("InPursuit", false);
                StartCoroutine(WaitBeforeReturn());
            }
        }


        private void AttackTarget(Vector3 toTarget)
        {
            var toTargetRotation = Quaternion.LookRotation(toTarget);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                toTargetRotation,
                180 * Time.deltaTime
                );
            //Debug.Log("Attack!");
            mEnemyKontrol.StopFollowTarget();


            mEnemyKontrol.Animator.SetTrigger(HashAttack);
        }

        private void FollowTarget()
        {

            mEnemyKontrol.Animator.SetBool(HashInPursuit, true);
            mEnemyKontrol.FollowTarget(mFollowTarget.transform.position);

        }

        private void CheckIfNearBase()
        {
            Vector3 toBase = mOriginalPosition - transform.position;
            toBase.y = 0;

            bool nearBase = toBase.magnitude < 0.01f;

            mEnemyKontrol.Animator.SetBool(HashNearBase, nearBase);

            if (nearBase)
            {
                Quaternion targetRotation = Quaternion.RotateTowards(transform.rotation, mOriginalRotation, 360 * Time.deltaTime);

                transform.rotation = targetRotation;
            }
        }

        private IEnumerator WaitBeforeReturn()
        {
            yield return new WaitForSeconds(timeToWaitOnPursuit);
            //mNavMeshAgent.isStopped = false;
            mEnemyKontrol.FollowTarget(mOriginalPosition);
        }

      


#if UNITY_EDITOR
        // gizmoda karakter se�iliyken �al���r
        private void OnDrawGizmosSelected()
        {
            // d��man�n g�r�� a��s�n� g�rselle�tirme
            Color c = new Color(0.8f, 0, 0, 0.4f);
            UnityEditor.Handles.color = c;

            Vector3 rotatedForward = Quaternion.Euler(0,-playerScanner.detectionAngle * 0.5f,0) * transform.forward;

            UnityEditor.Handles.DrawSolidArc(transform.position,Vector3.up,rotatedForward,playerScanner.detectionAngle, playerScanner.detectionRadius);

            UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedForward, 360, playerScanner.meleeDetectionRadius);

        }
#endif

    }

}

