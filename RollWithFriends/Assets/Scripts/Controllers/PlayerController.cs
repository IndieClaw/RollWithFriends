using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerController : MonoBehaviour
{
    #region Fields and properties

    [SerializeField] Rigidbody rb;

    private Checkpoint lastCheckpointReached;

    public static event Action<PlayerController> OnPlayerReachedEnd = delegate { };

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
                    .Where(c => c.checkpointType == Checkpoint.CheckpointType.Start)
                    .FirstOrDefault()
                    .respawnPoint.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TagCheckpoint))
        {
            var cp = other.transform.GetComponent<Checkpoint>();
            lastCheckpointReached = cp;

            if (cp.checkpointType == Checkpoint.CheckpointType.End)
            {
                OnPlayerReachedEnd(this);
            }
        }

        if (other.CompareTag(Constants.TagDeath))
        {
            Respawn();
        }
    }

    #endregion
}
