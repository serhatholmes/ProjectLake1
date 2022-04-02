using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    public partial class Damageable : MonoBehaviour
    {
        
        [Range(0.0f,360.0f)]
        public float hitAngle = 360.0f;
        public float invulnerabilityTime = 0.5f;
        public int maxHitPoints;
        public int CurrentHitPoints { get; private set; }
        public List<MonoBehaviour> onDamageMessageReceivers;

        private bool IsInvulnerable;
        private float TimeSinceLastHit = 0f;
       

        private void Awake()
        {
            CurrentHitPoints = maxHitPoints;

            onDamageMessageReceivers.Add(FindObjectOfType<QuestManager>());
        }

        private void Update()
        {
            if(IsInvulnerable)
            {
                TimeSinceLastHit += Time.deltaTime;

                if(TimeSinceLastHit >= invulnerabilityTime)
                {
                    IsInvulnerable = false;
                    TimeSinceLastHit = 0;
                }
            }
        }

        public void ApplyDamage(DamageMessage data)
        {
            //Debug.Log("Applying damage!");
            //Debug.Log(data.amount);
            //Debug.Log(data.damager);
            //Debug.Log(data.damageSource);

            if(CurrentHitPoints <=0 || IsInvulnerable)
            {
                return;
            }

            Vector3 positionToDamager = data.damageSource.transform.position - transform.position;
            positionToDamager.y = 0;

            if(Vector3.Angle(transform.forward,positionToDamager)>hitAngle*0.5f)
            {
                //Debug.Log("not hitting");
                return;
            }

            IsInvulnerable = true;
            CurrentHitPoints -= data.amount;

            //Debug.Log(CurrentHitPoints);
            

            var messageType = CurrentHitPoints <= 0 ? MessageType.DEAD : MessageType.DAMAGED;
        
            for(int i=0;i<onDamageMessageReceivers.Count; i++)
            {
                var receiver = onDamageMessageReceivers[i] as iMessageReceiver;
                receiver.OnReceiveMessage(messageType,this,data);
              
            }
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = new Color(0.0f, 0.0f, 1.0f, 0.5f);

            Vector3 rotatedForward = Quaternion.AngleAxis(-hitAngle * 0.5f, transform.up)*transform.forward;

            UnityEditor.Handles.DrawSolidArc(transform.position, transform.up, rotatedForward, hitAngle, 1.0f);
        }


#endif

    }
}

