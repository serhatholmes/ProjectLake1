using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace SoulsLike
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public float detectionRadius = 8.0f;
        public float detectionAngle = 140.0f;
        public float timeToStopPursuit = 2.0f;

        private PlayerKontrol mTarget;
        private NavMeshAgent mNavMestAgent;
        private float mTimeSinceLostTarget = 0;

        private void Awake()
        {
            mNavMestAgent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            //Debug.Log(PlayerKontrol.Instance);
            LookForPlayer();
            var target = LookForPlayer();

            //if (!mTarget) { return; }
            if(mTarget==null)
            {
                if(target != null)
                {
                    mTarget = target;
                }
            }
            else

            {
                mNavMestAgent.SetDestination(mTarget.transform.position);

                if (target == null)
                {
                    mTimeSinceLostTarget += Time.deltaTime;
                    if(mTimeSinceLostTarget >= timeToStopPursuit)
                    {
                        mTarget = null;
                        Debug.Log("stopping the enemy!");
                    }
                }
                else
                {
                    mTimeSinceLostTarget = 0;
                }

                //Vector3 targetPosition = mTarget.transform.position;
                //Debug.Log(targetPosition);
              
            }



        }

        private PlayerKontrol LookForPlayer()
        {

            if (PlayerKontrol.Instance == null)
            {
                return null;
            }

            //aralarýndaki mesafe için
            Vector3 enemyPosition = transform.position;
            Vector3 toPlayer = PlayerKontrol.Instance.transform.position - enemyPosition;
            toPlayer.y = 0;

            if (toPlayer.magnitude <= detectionRadius)
            {
                //Debug.Log("Detecting the player!");

                if (Vector3.Dot(toPlayer.normalized, transform.forward) >
                   Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
                {
                    Debug.Log("Player Has been detected!!!!");
                    return PlayerKontrol.Instance;
                }
            }
            /*else
            {
               // Debug.Log("Where are you?");
                
            }
            */
            return null;
        }


#if UNITY_EDITOR
        // gizmoda karakter seçiliyken çalýþýr
        private void OnDrawGizmosSelected()
        {
            // düþmanýn görüþ açýsýný görselleþtirme
            Color c = new Color(0.8f, 0, 0, 0.4f);
            UnityEditor.Handles.color = c;

            Vector3 rotatedForward = Quaternion.Euler(0,-detectionAngle * 0.5f,0) * transform.forward;

            UnityEditor.Handles.DrawSolidArc(transform.position,Vector3.up,rotatedForward,detectionAngle, detectionRadius);

        }
#endif

    }

}

