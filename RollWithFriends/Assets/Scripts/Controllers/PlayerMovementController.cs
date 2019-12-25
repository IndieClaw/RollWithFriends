using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Fields and properties
    [SerializeField] private float speed = 8;
    [SerializeField] private int jumpForce = 300;

    [SerializeField] private Rigidbody childRb;

    bool isMoving = false;

    private bool canJump;

    #endregion

    #region Public methods

    #endregion


    #region Private methods	

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            isMoving = true;
        }else
        {
            isMoving = false;
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

    }
    // void FixedUpdate()
    // {
    //     if (!hasAuthority)
    //     {
    //         return;
    //     }        

    //     if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
    //     {
    //         MovePlayer(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));            
    //     }

    // }

    void Jump()
    {
        if (canJump)
        {
            childRb.AddForce(new Vector3(0, jumpForce, 0));
            canJump = false;
        }
    }

    void Move(float horizontalInput, float verticalInput)
    {
        // camera orientation
        Vector3 verticalOrientation = Input.GetAxis("Vertical") * CameraManager.instance.transform.forward;
        Vector3 horizontalOrientation = Input.GetAxis("Horizontal") * CameraManager.instance.transform.right;

        var desiredMoveDirection = (verticalOrientation + horizontalOrientation).normalized;

        childRb.AddForce(desiredMoveDirection * speed);
    }


    private IEnumerator AssignCameraTarget()
    {
        yield return new WaitForSeconds(.01f);

        CameraManager.instance.target = gameObject.transform;
    }
    #endregion

    #region Colision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }
    #endregion
}
