using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike

{

    public class PlayerKontrol : MonoBehaviour
    {
        public float speed;
        public float rotationSpeed;

        private Rigidbody rb;
        private Vector3 mMovement;
        private Quaternion rotation;

        // Start is called before the first frame update
        void Start()
        {

            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            mMovement = new Vector3(horizontalInput, 0, verticalInput);
            mMovement.Normalize();



            Vector3 desiredForward = Vector3.RotateTowards(transform.forward,mMovement,Time.fixedDeltaTime*rotationSpeed,0);

           // Debug.Log("FORWARD:" + transform.forward.magnitude);
           // Debug.Log("MOVEMENT:" + mMovement.magnitude);


            rotation = Quaternion.LookRotation(desiredForward);

            rb.MovePosition(rb.position + mMovement * speed * Time.fixedDeltaTime);
            rb.MoveRotation(rotation);

        }
    }
}
