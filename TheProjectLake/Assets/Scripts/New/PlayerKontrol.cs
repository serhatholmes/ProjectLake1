using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike

{

    public class PlayerKontrol : MonoBehaviour
    {
        const float kAcceleration = 20f;
        const float kDeceleration = 200f;

        public float maxForwardSpeed = 4;
        public float rotationSpeed;


        private PlayerInput mPlayerInput;
        private CharacterController chCont;
        private Vector3 mMovement;
        
        
        private CameraController mainCamController;
        private Animator mAnimator;

        private readonly int HashForwardSpeed = Animator.StringToHash("ForwardSpeed");

        private Quaternion mTargetRotation;

        private float desiredForwardSpeed;
        private float forwardSpeed;

        private void Awake()
        {
            chCont = GetComponent<CharacterController>();
            mainCamController = GetComponent<CameraController>();
            mPlayerInput = GetComponent<PlayerInput>();
            mAnimator = GetComponent<Animator>();

        }

    
        // Update is called once per frame
        void FixedUpdate()
        {
            ComputeMovement();
            ComputeRotation();

            if(mPlayerInput.IsMoveInput)
            {
                transform.rotation = mTargetRotation;

            }
            /* float horizontalInput = Input.GetAxis("Horizontal");
             float verticalInput = Input.GetAxis("Vertical");

             mMovement.Set(horizontalInput,0,verticalInput);
             */
            /*
                Vector3 moveInput = mPlayerInput.MoveInput;


                Quaternion camRotation = mainCam.transform.rotation;

                Vector3 targetDirection = camRotation * moveInput;
                targetDirection.y = 0;

                */
            //chCont.Move(targetDirection.normalized * speed * Time.fixedDeltaTime);

            //Quaternion rotation = Quaternion.LookRotation(desiredForward);

            //chCont.transform.rotation = Quaternion.Euler(0, camRotation.eulerAngles.y, 0);
            //rb.MoveRotation(rotation);

        }

        private void ComputeMovement()
        {
            Vector3 moveInput = mPlayerInput.MoveInput.normalized;

            desiredForwardSpeed = moveInput.magnitude * maxForwardSpeed;

            float acceleration = mPlayerInput.IsMoveInput ? kAcceleration : kDeceleration;

            forwardSpeed = Mathf.MoveTowards(forwardSpeed, desiredForwardSpeed, Time.fixedDeltaTime * 25);

            mAnimator.SetFloat(HashForwardSpeed, forwardSpeed);

        }

        private void ComputeRotation()
        {
            Vector3 moveInput = mPlayerInput.MoveInput.normalized;

            Vector3 cameraDirection = Quaternion.Euler(0,mainCamController.freeLookCamera.m_XAxis.Value,0) * Vector3.forward;

            Quaternion movementRotation = Quaternion.FromToRotation(Vector3.forward, moveInput);

            Quaternion targetRotation = Quaternion.LookRotation(movementRotation * cameraDirection);

            mTargetRotation = targetRotation;

        }

    }
}
