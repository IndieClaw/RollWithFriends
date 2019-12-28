using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Fields and properties

    [SerializeField] Rigidbody rb;

    private Checkpoint lastCheckpointReached;

    #endregion

    #region Public methods

    #endregion


    #region Private methods	

    void Start()
    {

    }

    void Update()
    {

    }
    void Respawn()
    {        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = lastCheckpointReached.respawnPoint.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TagCheckpoint))
        {
            lastCheckpointReached = other.transform.GetComponent<Checkpoint>();
        }

        if (other.CompareTag(Constants.TagDeath))
        {
            if (lastCheckpointReached != null)
            {
                Respawn();
            }
        }
    }

    #endregion
}
