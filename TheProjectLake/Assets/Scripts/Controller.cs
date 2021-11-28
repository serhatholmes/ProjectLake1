using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    float inputX;
    float inputY;

    public Transform Player;

    Animator anim;

    Vector3 StickDirection;

    Camera mainCam;

    public float damp;

    [Range(1,20)]
    public float rotationSpeed;



    void Start()
    {
        anim = GetComponent<Animator>();
        mainCam = Camera.main;

    }

    

    private void LateUpdate()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        //karakterin gidece�i y�n i�in
        StickDirection = new Vector3(inputX, 0, inputY);

        InputMove();
        InputRotation();


    }

    void InputMove()
    {
        //h�z�n eksi olmas� yerine 0-1 aras�nda kalmas� i�in
        anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, 1).magnitude, damp , Time.deltaTime*10);
    }

    void InputRotation()
    {
        //d�nmeyi takip edecek
        Vector3 rotOfset = mainCam.transform.TransformDirection(StickDirection);
        rotOfset.y = 0;

        Player.forward = Vector3.Slerp(Player.forward, rotOfset,Time.deltaTime*rotationSpeed);
    }
}
