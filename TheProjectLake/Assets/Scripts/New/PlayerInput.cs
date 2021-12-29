using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    public class PlayerInput : MonoBehaviour
    {

        private Vector3 mMovement;
        private bool mIsAttack;

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

        void Update()
        {

            mMovement.Set(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
       
            if(Input.GetButtonDown("Fire1") && mIsAttack==false)
            {
                // Debug.Log("attacking");
                StartCoroutine(AttackAndWait());
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


