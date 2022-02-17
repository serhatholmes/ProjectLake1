using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike

{

    public class PlayerKontrol : MonoBehaviour
    {
        public static PlayerKontrol Instance
        {
            get
            {
                return sInstance;
            }
        }

        public MeleeWeapons meleeWeapon;
        const float kAcceleration = 20f;
        const float kDeceleration = 200f;

        public float maxForwardSpeed = 4;
        public float rotationSpeed;

        public float mMaxRotationSpeed = 1200;
        public float mMinRotationSpeed = 800;
        public float gravity = 20.0f;


        private PlayerInput mPlayerInput;
        private CharacterController chCont;
        private Vector3 mMovement;
        private static PlayerKontrol sInstance;
        
        private CameraController mainCamController;
        private Animator mAnimator;

        private readonly int HashForwardSpeed = Animator.StringToHash("ForwardSpeed");
        private readonly int HashMeleeAttack = Animator.StringToHash("MeleeAttack");

        private Quaternion mTargetRotation;

        private float desiredForwardSpeed;
        private float forwardSpeed;
        private float mVerticalSpeed;

        private void Awake()
        {
            chCont = GetComponent<CharacterController>();
      
            mPlayerInput = GetComponent<PlayerInput>();
            mAnimator = GetComponent<Animator>();
            mainCamController = Camera.main.GetComponent<CameraController>();

            sInstance = this;
        }

    
        // Update is called once per frame
        void FixedUpdate()
        {
            ComputeForwardMovement();
            ComputeVerticalMovement();
            ComputeRotation();

            if(mPlayerInput.IsMoveInput)
            {
                float rotationSpeed = Mathf.Lerp(mMaxRotationSpeed, mMinRotationSpeed, forwardSpeed / desiredForwardSpeed);
                mTargetRotation = Quaternion.RotateTowards(
                    transform.rotation,
                    mTargetRotation,
                    rotationSpeed * Time.fixedDeltaTime);

                transform.rotation = mTargetRotation;

            }

            mAnimator.ResetTrigger(HashMeleeAttack);

            if(mPlayerInput.IsAttack)
            {
                //Debug.Log("is attacking");
                mAnimator.SetTrigger(HashMeleeAttack);
                meleeWeapon.AttackBegin();
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

        private void ComputeVerticalMovement()
        {
            mVerticalSpeed = -gravity;
        }


        private void OnAnimatorMove()
        {
            Vector3 movement = mAnimator.deltaPosition;
            movement += mVerticalSpeed * Vector3.up * Time.fixedDeltaTime;
            
            chCont.Move(movement);
        }


        private void ComputeForwardMovement()
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

            Vector3 cameraDirection = Quaternion.Euler(0, mainCamController.PlayerCam.m_XAxis.Value, 0) * Vector3.forward;

            
            Quaternion targetRotation;

            if (Mathf.Approximately(Vector3.Dot(moveInput, Vector3.forward), -1.0f))
            {
                targetRotation = Quaternion.LookRotation(-cameraDirection);
            }
            else
            {
                Quaternion movementRotation = Quaternion.FromToRotation(Vector3.forward, moveInput);
                targetRotation = Quaternion.LookRotation(movementRotation * cameraDirection);
            }

            mTargetRotation = targetRotation;

        }

    }
}
