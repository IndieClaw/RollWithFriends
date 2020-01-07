using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    Dictionary<Transform, Material> obstructionDictionary = new Dictionary<Transform, Material>();

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
        Cursor.lockState = CursorLockMode.Locked;
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
            HandleMeshTransparencyAll();
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

    void HandleMeshTransparencyAll()
    {
        var dir = target.position - pivot.transform.position;
        dir.Normalize();

        RaycastHit[] hits;
        hits = Physics.RaycastAll(origin: pivot.transform.position, direction: dir * 10, maxDistance: 4.5f);

        if (hits.Any())
        {
            foreach (var obj in hits)
            {
                if (!obstructionDictionary.ContainsKey(obj.transform))
                {
                    var mesh = obj.transform.gameObject.GetComponent<MeshRenderer>();
                    obstructionDictionary.Add(obj.transform, mesh.material);
                    mesh.material = transparencyMaterial;
                    mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                }
            }
        }
        else
        {
            if (obstructionDictionary.Any())
            {
                foreach (var obs in obstructionDictionary)
                {
                    var mesh = obs.Key.GetComponent<MeshRenderer>();
                    mesh.material = obs.Value;
                    mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                }

                obstructionDictionary.Clear();
            }
        }
    }


    #endregion
}
