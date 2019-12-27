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
    bool canMove = false;

    private bool canJump;

    #endregion

    #region Public methods

    #endregion


    #region Private methods	

    void Start()
    {
        SubscribeEvents();
    }

    void Update()
    {
        if(!canMove)
            return;

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
        if(!canMove)
            return;

        if (isMoving)
        {
            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

    }    

    private void OnDisable() 
    {
        UnSubscribeEvents();
        
    }

    private void OnDestroy() 
    {
        UnSubscribeEvents();
    }

    private void SubscribeEvents()
    {
        LevelManager.OnLevelStarted += AllowMovement;
    }

    private void UnSubscribeEvents()
    {
        LevelManager.OnLevelStarted -= AllowMovement;
    }

    private void AllowMovement()
    {
        canMove = true;
    }

    void Jump()
    {
        if (canJump || childRb.velocity.y == 0)
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

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = false;
        }
    }
    #endregion
}
