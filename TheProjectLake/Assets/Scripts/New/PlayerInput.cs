using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    public class PlayerInput : MonoBehaviour
    {

        public static PlayerInput Instance { get { return mInstance; } }
        public float distanceToInteractWithNpc= 2.4f;

        public static  PlayerInput mInstance;
        private Vector3 mMovement;
        private bool mIsAttack;

        private Collider mOptionClickTarget;
        //public bool mIsTalk;

        public Collider OptionClickTarget { get { return mOptionClickTarget;}}
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

        private void Awake() {
            
            mInstance = this;
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
                    StartCoroutine(TriggerAttack());
                }
        }

        private void HandleRightMouseBtnDown()
        {
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                bool hasHit = Physics.Raycast(ray, out RaycastHit hit);

                if(hasHit)
                {
                   //mOptionClickTarget = hit.collider;
                   StartCoroutine(TriggerOptionTarget(hit.collider));
                }
        }

        private IEnumerator TriggerOptionTarget(Collider other)
        {
            mOptionClickTarget = other;
            yield return new WaitForSeconds(0.03f);
            mOptionClickTarget = null;
        }
        private IEnumerator TriggerAttack()
        {
            mIsAttack = true;
            yield return new WaitForSeconds(0.03f);
            mIsAttack = false;
        }
    }

}


