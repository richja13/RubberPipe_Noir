using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [Header("HeadBob Parameters")]

    [SerializeField] private bool _enalbe = true;
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmmount = 0.05f;
    [SerializeField] private float SprintBobSpeed = 19f;
    [SerializeField] private float SprintBobAmmount = 0.1f;
    [SerializeField] private float CrouchBobSpeed = 10f;
    [SerializeField] private float CrouchBobAmmount = 0.025f;
    private float defoultYPos = 0;
    private float timer;
    Rigidbody rb;
    [SerializeField] private Transform _camera = null;
    private CharacterController _controller;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        defoultYPos = _camera.transform.localPosition.y;
      
    }    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(_enalbe)
      {
        HandleHeadBob();
        
      }
    }

    private void HandleHeadBob()
    {
        if(Mathf.Abs(PlayerController.Instance.move.x) > 0.1f || Mathf.Abs(PlayerController.Instance.move.z) > 0.1f)
        {
            Debug.Log("JAZDA");
            timer += Time.deltaTime * (PlayerController.Instance.isCrouching ? CrouchBobSpeed : PlayerController.Instance.isSpriting ? SprintBobSpeed : walkBobSpeed);
            _camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x
            , defoultYPos + Mathf.Sin(timer) * (PlayerController.Instance.isCrouching ? CrouchBobAmmount : PlayerController.Instance.isSpriting ? SprintBobAmmount : walkBobAmmount), 
            _camera.transform.localPosition.z);
        }
    }
}
