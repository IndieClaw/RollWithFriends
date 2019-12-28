using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

        if (lastCheckpointReached != null)
        {
            transform.position =
                lastCheckpointReached.respawnPoint.transform.position;
        }
        else
        {
            transform.position =
                GameObject.FindObjectsOfType<Checkpoint>()
                    .Where(c =>c.checkpointType == Checkpoint.CheckpointType.Start)
                    .FirstOrDefault()
                    .respawnPoint.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TagCheckpoint))
        {
            lastCheckpointReached = other.transform.GetComponent<Checkpoint>();
        }

        if (other.CompareTag(Constants.TagDeath))
        {
            Respawn();
        }
    }

    #endregion
}
