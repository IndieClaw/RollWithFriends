using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour
{
    public GameObject playerUnitToFollow;

    private Vector3 vectorOriginalCameraRotation = new Vector3(25,0,0);
    private Quaternion originalCameraRotation;
    [SerializeField]
    public Vector3 offset;

    void Start ()
	{
        originalCameraRotation = Quaternion.Euler(vectorOriginalCameraRotation);
    }

    private void Update()
    {
        
    }
    
    void LateUpdate ()
	{
        if (playerUnitToFollow != null)
        {
            //transform.position = playerUnitToFollow.transform.position + offset;

            //***************************************************************************

            var y = Input.GetAxis("VerticalRight") * 179;
            var x = Input.GetAxis("HorizontalRight") * 179;


            Quaternion rotation = Quaternion.Euler(y, x, 0f);

            Vector3 position = rotation * offset + playerUnitToFollow.transform.position;

            transform.position = position;
            gameObject.transform.LookAt(new Vector3(playerUnitToFollow.transform.position.x, playerUnitToFollow.transform.position.y +1.5f, playerUnitToFollow.transform.position.z));            
        }
        
        if (Input.GetButtonDown("ResetCamera"))
        {            
            gameObject.transform.rotation = originalCameraRotation;
        }
    }


}
