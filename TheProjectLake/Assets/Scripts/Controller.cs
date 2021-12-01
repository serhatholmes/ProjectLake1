using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    float inputX;
    float inputY;

    float normalFov;
    public float sprintFov;

    public Transform Player;

    Animator anim;

    Vector3 StickDirection;

    Camera mainCam;

    public float damp;

    [Range(1,20)]
    public float rotationSpeed;

    float maxSpeed;

    public KeyCode SprintButton = KeyCode.LeftShift;
    public KeyCode WalkButton = KeyCode.LeftControl;


    void Start()
    {
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
        normalFov = mainCam.fieldOfView;

    }

    

    private void LateUpdate()
    {
     

        InputMove();
        InputRotation();
        Movement();

    }

    void Movement()
    {
        //y�n belirlemek i�in
        StickDirection = new Vector3(inputX, 0, inputY);

        if (Input.GetKey(SprintButton))
        {
            //kameray� uzakla�t�rma,ko�ma efekti i�in
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, sprintFov, Time.deltaTime * 2);

            maxSpeed = 1.5f;
            inputX =2 * Input.GetAxis("Horizontal");
            inputY =2 * Input.GetAxis("Vertical");
        }

        else if(Input.GetKey(WalkButton))
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, Time.deltaTime * 2);
            maxSpeed = 0.3f;
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");
        }

        else
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, Time.deltaTime * 2);
            maxSpeed = 1f;
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");
        }


       
        
    }

    void InputMove()
    {
        //h�z�n eksi olmas� yerine 0-1 aras�nda kalmas� i�in
        anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, maxSpeed).magnitude, damp , Time.deltaTime*10);
    }

    void InputRotation()
    {
        //d�nmeyi takip edecek
        Vector3 rotOfset = mainCam.transform.TransformDirection(StickDirection);
        rotOfset.y = 0;

        Player.forward = Vector3.Slerp(Player.forward, rotOfset,Time.deltaTime*rotationSpeed);
    }
}
