using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockPickScript : MonoBehaviour
{
    public GameObject PressE;
    bool OnDoorCollision = false;
    public GameObject LockPics;
    public LayerMask DoorMask;
    public bool LockPicsOpen;
    public PlayerController PContrl;
    public MouseLook ML;

    // Update is called once per frame
    void Update()
    {

        if(!LockPicsOpen)
        {
            DetectDoors();
        }

       


        if(OnDoorCollision && !LockPicsOpen && !LockPics.active)
        {
            PressE.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("KEY E");
                LockPicsOpen = true;
                LockPics.SetActive(true);
                PressE.SetActive(false);
                FlashLightScript.instance.FlashOff = true;
                ML.enabled = false;
                PContrl.enabled = false;
                //Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else
        {
            PressE.SetActive(false);
            if(LockPicsOpen && Input.GetKeyDown(KeyCode.E) && LockPics.active)
            {
                LockPics.SetActive(false);
                LockPicsOpen = false;
                PressE.SetActive(true);
                Debug.Log("Closed");
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                ML.enabled = true;
                PContrl.enabled = true;
            }
            // Cursor.visible = false;
            // Cursor.lockState = CursorLockMode.Locked;
        }


       
    }


    void DetectDoors()
    {
        RaycastHit hit = new RaycastHit();
        float distance = 3f;

        
        if(Physics.Raycast(transform.position, transform.forward, out hit, distance,DoorMask))
        {
            OnDoorCollision = true;
        }
        else
        {
            OnDoorCollision = false;
        } 
    }
}
