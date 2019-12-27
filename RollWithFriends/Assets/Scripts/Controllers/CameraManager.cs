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

    Transform obstruction;
    Material obstructionMaterial;
    MeshRenderer meshRenderer;
    [SerializeField] Material transparencyMaterial;

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
        Cursor.visible = false;
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
            HandleRotations(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            HandleMeshTransparency();
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

    void HandleMeshTransparency()
    {
        RaycastHit hit;

        var dir = target.position - pivot.transform.position;
        dir.Normalize();

        if (Physics.Raycast(pivot.transform.position, dir * 10, out hit, 4.5f))
        {
            if (hit.collider.gameObject != null)
            {
                // Save the material for the object, if the raycast is collidig with the same object we dont want to get the mat.                
                if (obstruction != hit.transform)
                {
                    meshRenderer = hit.transform.gameObject.GetComponent<MeshRenderer>();
                    obstructionMaterial = meshRenderer.material;
                }

                obstruction = hit.transform;

                meshRenderer.material = transparencyMaterial;
                meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }
        }
        else
        {
            if (obstruction != null)
            {
                meshRenderer.material = obstructionMaterial;
                meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
        }
    }


    #endregion
}
