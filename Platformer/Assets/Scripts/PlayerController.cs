using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float _moveSpeed = 10f;
    private float _jumpForce = 15f;
    private float _gravityScale = 5f;
    private float _knockBackForce = 10f;
    private float _knockBackTime = 0.25f;
    private float _knockBackCounter;
    private float _rotateSpeed = 10f;
    public Animator anim;

    public CharacterController controller;
    public Vector3 moveDirection;
    public Transform pivot;
    public GameObject playerModel;

    

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
        if (_knockBackCounter <= 0)
        {
            float yStore = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            moveDirection = moveDirection.normalized * _moveSpeed;
            moveDirection.y = yStore;

            if (controller.isGrounded)
            {
                moveDirection.y = 0f;
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = _jumpForce;
                }
            }
        }
        else
        {
            _knockBackCounter -= Time.deltaTime;
        }
        moveDirection.y = moveDirection.y + (Physics.gravity.y * _gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

        // Move the player in different directions based on camera look direction
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, _moveSpeed * Time.deltaTime);
        }


        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));


    }
    public void KnockBack(Vector3 direction)
    {
        _knockBackCounter = _knockBackTime;

        moveDirection = direction * _knockBackForce;
        moveDirection.y = _knockBackForce;

    }

}
