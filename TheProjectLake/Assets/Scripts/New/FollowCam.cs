using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private Transform target;



 

    void LateUpdate()
    {

        if(!target)
        {
            return;
        }

        float currentRotationAngle = transform.eulerAngles.y;
        float wantedRotationAngle = target.eulerAngles.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle,wantedRotationAngle,0.1f);

        transform.position = new Vector3(target.position.x,72.0f,target.position.z);
        
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        Vector3 rotatedPosition = currentRotation * Vector3.forward;

        transform.position -= rotatedPosition * 10;

        transform.LookAt(target);
    }
}
