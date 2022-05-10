using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{

    public class MeleeWeapons : MonoBehaviour
    {
        [System.Serializable]
        public class AttackPoint
        {
            public float radius;
            public Vector3 offset;
            public Transform rootTransform;
        }

        public LayerMask targetLayers;
        public int damage = 10;
        public AttackPoint[] attackPoints = new AttackPoint[0];
        public RandomAudioPlayer swingAudio;
        public RandomAudioPlayer impactAudio;
      

        private bool IsAttack = false;
        private Vector3[] originalAttackPos;
        private RaycastHit[] mRayCastHitCache = new RaycastHit[32];
        private GameObject mOwner;


        private void FixedUpdate()
        {
            if (IsAttack)
            {
                for (int i = 0; i < attackPoints.Length; i++)
                {
                    AttackPoint ap = attackPoints[i];
                    Vector3 worldPos = ap.rootTransform.position+ap.rootTransform.TransformVector(ap.offset);
                    Vector3 attackVector = (worldPos - originalAttackPos[i]).normalized;

                    Ray r = new Ray(worldPos, attackVector);
                    Debug.DrawRay(worldPos, attackVector, Color.red, 4.0f);

                    int contacts = Physics.SphereCastNonAlloc(r,ap.radius,mRayCastHitCache,
                        attackVector.magnitude, ~0, QueryTriggerInteraction.Ignore);

                    for(int k=0;k<contacts; k++)
                    {
                        Collider col = mRayCastHitCache[k].collider;

                        if(col != null)
                        {
                            //Debug.Log("hit!");
                            CheckDamage(col, ap);
                        }
                    }

                    originalAttackPos[0] = worldPos;


                }
            }
        }

        private void CheckDamage(Collider other, AttackPoint ap)
        {

            if ((targetLayers.value & (1 << other.gameObject.layer)) == 0)
            {
                return;
            }
            Damageable damageable = other.GetComponent<Damageable>();

            //Debug.Log("we are hitting correct layer");

            if(damageable != null)
            {

                Damageable.DamageMessage data;
                data.amount = damage;
                data.damager = this;
                
                data.damageSource = mOwner;
                if(impactAudio != null)
                {
                    impactAudio.PlayRandomClip();
                }
                damageable.ApplyDamage(data);
            }

        }

        public void SetOwner(GameObject owner)
        {
            mOwner = owner;
        }


        public void AttackBegin()
        {
            swingAudio.PlayRandomClip();
            IsAttack = true;
            originalAttackPos = new Vector3[attackPoints.Length];

            for(int i=0; i<attackPoints.Length; i++)
            {
                AttackPoint ap = attackPoints[i];
                originalAttackPos[i] = ap.rootTransform.position + ap.rootTransform.TransformDirection(ap.offset);
            }
        }

        public void AttackEnd()
        {
            IsAttack = false;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            foreach(AttackPoint attackPoint in attackPoints)

            {
                if(attackPoint.rootTransform != null)
                {

                    Vector3 worldPosition = attackPoint.rootTransform.TransformVector(attackPoint.offset);

                    Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.6f);
                    Gizmos.DrawSphere(attackPoint.rootTransform.position + worldPosition, attackPoint.radius);
                }
            }
        }

#endif
    }

    

}
