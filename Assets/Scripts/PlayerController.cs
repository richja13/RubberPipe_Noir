using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float Speed;
    public float gravity = - 9.81f;
    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;
    bool isGrouned;
    public GameObject mCamera;
    public GameObject mCameraHolder;
    float Crouchheight;
    float height;
    public static PlayerController Instance;
    public bool isSpriting;
    public bool isCrouching;
    public Vector3 move;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Crouchheight = mCameraHolder.transform.position.y - 0.7f;
        height = mCameraHolder.transform.position.y;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        Crouch();
       
        if(Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            Speed = 15;
            isSpriting = true;
        }
        else
        {
            Speed = 8;
            isSpriting = false;
        }

        isGrouned = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrouned && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(Input.GetButtonDown("Jump") && isGrouned)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = (transform.right * x + transform.forward * z);
        velocity.y += gravity * Time.deltaTime;

        controller.Move(move * Speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);

    }

    

    void Crouch()
    {   
       
        if(Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
            isSpriting = false;
            mCameraHolder.transform.position = Vector3.Lerp(mCameraHolder.transform.position , new Vector3(mCameraHolder.transform.position.x,Crouchheight,mCameraHolder.transform.position.z), 10 * Time.deltaTime);
        }
        else
        {
            isCrouching = false;
             mCameraHolder.transform.position = Vector3.Lerp(mCameraHolder.transform.position , new Vector3(mCameraHolder.transform.position.x,height,mCameraHolder.transform.position.z), 10 * Time.deltaTime);
        }
    }
}
