using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    public class PlayerInput : MonoBehaviour
    {

        public float distanceToInteractWithNpc= 2.4f;
        private Vector3 mMovement;
        private bool mIsAttack;
        public bool mIsTalk;

        public Vector3 MoveInput
        {
            get
            {
                return mMovement;
            }
        }

        public bool IsMoveInput
        {
            get
            {
                return !Mathf.Approximately(MoveInput.magnitude, 0);
            }
        }

        public bool IsAttack
        {
            get
            {
                return mIsAttack;
            }
        }

        public bool isTalk
        {
            get
            {
                return mIsTalk;
            }
        }

        void Update()
        {

            mMovement.Set(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
       
            bool isLeftMouseClick = Input.GetMouseButtonDown(0);
            bool isRightMouseClick = Input.GetMouseButtonDown(1);

            if(isLeftMouseClick)
            {
                
                HandleLeftMouseBtnDown();
            }

            if(isRightMouseClick)
            {
                HandleRightMouseBtnDown();
            }
        }

        private void HandleLeftMouseBtnDown()
        {
            // Debug.Log("attacking");
                if(!mIsAttack)
                {
                    StartCoroutine(AttackAndWait());
                }
        }

        private void HandleRightMouseBtnDown()
        {
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                bool hasHit = Physics.Raycast(ray, out RaycastHit hit);

                if(hasHit && hit.collider.CompareTag("QuestGiver"))
                {
                    var distanceToTarget = (transform.position - hit.transform.position).magnitude;
                    //var distance = Vector3.Distance(transform.position, hit.transform.position);

                    if(distanceToTarget <= distanceToInteractWithNpc)
                    {
                        mIsTalk=true;
                    }
                }
        }

        private IEnumerator AttackAndWait()
        {
            mIsAttack = true;
            yield return new WaitForSeconds(0.03f);
            mIsAttack = false;
        }
    }

}


