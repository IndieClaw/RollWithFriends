using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Fields and properties
    public static CameraManager instance;

    [SerializeField] Vector3 cameraPlayerOffset;

    [SerializeField]
    public Transform target;

    [SerializeField]
    float controllerSpeed = 8;

    [SerializeField] private Transform pivot;
    //[HideInInspector] public Transform cameraTransform;

    float turnSmoothing = .1f;

    [SerializeField]
    float minAngle = -90;

    [SerializeField]
    float maxAngle = 90;

    float smoothX;
    float smoothVelocityX;
    float smoothY;
    float smoothVelocityY;

    float lookAngle;
    float tiltAngle = 20;
    
    #endregion

    #region Public methods

    #endregion


    #region Private methods	
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }


    void Start()
    {
        pivot.transform.position += cameraPlayerOffset;
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        if (target != null)
        {
            FollowTarget();
            HandleRotations(Input.GetAxis("HorizontalRight"), Input.GetAxis("VerticalRight"));
        }
    }

    void FollowTarget()
    {
        transform.position = target.transform.position;
    }

    void HandleRotations(float vertical, float horizontal)
    {
        if (turnSmoothing > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, vertical, ref smoothVelocityX, turnSmoothing);
            smoothY = Mathf.SmoothDamp(smoothY, horizontal, ref smoothVelocityY, turnSmoothing);
        }
        else
        {
            smoothX = horizontal;
            smoothY = vertical;
        }

        lookAngle += smoothX * controllerSpeed;
        transform.rotation = Quaternion.Euler(0, lookAngle, 0);

        tiltAngle -= smoothY * controllerSpeed;
        tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
        pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
    }

    
    #endregion
}
