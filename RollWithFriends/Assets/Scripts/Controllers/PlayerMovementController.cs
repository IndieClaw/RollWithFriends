using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovementController : MonoBehaviourPun
{
    #region Fields and properties
    [SerializeField] private float speed = 8;

    [SerializeField] private int jumpForce = 300;

    [SerializeField] private Rigidbody rb;
    [SerializeField] CameraController cameraController;

    [SerializeField] PlayerController playerController;

    bool isMoving = false;
    bool canMove = false;

    private bool canJump = true;
    bool doIHaveControllOverThisPlayer = false;

    #endregion

    #region Public methods

    #endregion


    #region Private methods	

    void Start()
    {
        doIHaveControllOverThisPlayer = photonView.IsMine || photonView.ViewID == 0;
        if (!doIHaveControllOverThisPlayer)
        {
            return;
        }
        
        SubscribeEvents();
    }

    void Update()
    {
        if (!doIHaveControllOverThisPlayer)
            return;
            
        if (!canMove)
            return;

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    void FixedUpdate()
    {
        if (!canMove)
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
        playerController.OnPlayerResetLevel += RestrictMovement;
    }

    private void UnSubscribeEvents()
    {
        LevelManager.OnLevelStarted -= AllowMovement;
        playerController.OnPlayerResetLevel -= RestrictMovement;
    }

    private void RestrictMovement()
    {
        canMove = false;
    }

    private void AllowMovement()
    {
        canMove = true;
    }

    void Jump()
    {
        if (canJump || rb.velocity.y == 0)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
            canJump = false;
        }
    }

    void Move(float horizontalInput, float verticalInput)
    {
        // camera orientation
        Vector3 verticalOrientation = Input.GetAxis("Vertical") * cameraController.transform.forward;
        Vector3 horizontalOrientation = Input.GetAxis("Horizontal") * cameraController.transform.right;

        var desiredMoveDirection = (verticalOrientation + horizontalOrientation).normalized;

        rb.AddForce(desiredMoveDirection * speed);
    }


    private IEnumerator AssignCameraTarget()
    {
        yield return new WaitForSeconds(.01f);

        cameraController.target = gameObject.transform;
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
