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


        public int damage = 10;
        public AttackPoint[] attackPoints = new AttackPoint[0];

        public void AttackBegin()
        {
            Debug.Log("ATTACK");
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
