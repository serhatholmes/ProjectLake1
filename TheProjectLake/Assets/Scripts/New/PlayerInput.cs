using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    public class PlayerInput : MonoBehaviour
    {

        private Vector3 mMovement;

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

        void Update()
        {
            mMovement.Set(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        }
    }

}


