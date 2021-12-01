using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForIKLook : MonoBehaviour
{
    Animator anim1;

    Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        anim1 = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim1.SetLookAtWeight(1f, 0.5f, 1.4f, 0.5f,0.5f);

        Ray lookAtRay = new Ray(transform.position, mainCam.transform.forward);

        anim1.SetLookAtPosition(lookAtRay.GetPoint(20));


    }
}
