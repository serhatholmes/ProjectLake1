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
        //karakterin gideceði yön için
        StickDirection = new Vector3(inputX, 0, inputY);

        InputMove();
        InputRotation();


    }

    void InputMove()
    {
        //hýzýn eksi olmasý yerine 0-1 arasýnda kalmasý için
        anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, 1).magnitude, damp , Time.deltaTime*10);
    }

    void InputRotation()
    {
        //dönmeyi takip edecek
        Vector3 rotOfset = mainCam.transform.TransformDirection(StickDirection);
        rotOfset.y = 0;

        Player.forward = Vector3.Slerp(Player.forward, rotOfset,Time.deltaTime*rotationSpeed);
    }
}
