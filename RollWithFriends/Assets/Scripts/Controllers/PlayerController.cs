using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerController : MonoBehaviour
{
    #region Fields and properties

    [SerializeField] private Rigidbody rigidBody;

    private Checkpoint lastCheckpointReached;

    public static event Action OnPlayerReachedEnd = delegate { };

    public static event Action OnPlayerResetLevel = delegate { };
    bool endedLevel;

    #endregion

    #region Public methods

    #endregion


    #region Private methods	

    void Start()
    {        
        SubscriveEvents();
    }

    void Update()
    {
        if (Input.GetButtonDown(Constants.ButtonResetLevel))
        {
            OnPlayerResetLevel();

        }

        if (Input.GetButtonDown(Constants.ButtonResetCheckpoint))
        {
            if (!endedLevel)
            {
                RespawnAtLastCheckpoint();
            }
        }

    }

    void SubscriveEvents()
    {
        LevelManager.OnLevelWasReset += RestartAtStart;
    }

    void UnsubscribeEvents()
    {
        LevelManager.OnLevelWasReset -= RestartAtStart;
    }

    private void OnDestroy() 
    {
        UnsubscribeEvents();
    }

    private void OnDisable() 
    {
        UnsubscribeEvents();
    }

    void RestartAtStart()
    {
        endedLevel = false;
        Freeze(false);
        lastCheckpointReached = null;
        transform.position =
            GameObject.FindObjectsOfType<Checkpoint>()
                .Where(c => c.checkpointType == Checkpoint.CheckpointType.Start)
                .FirstOrDefault()
                .respawnPoint.transform.position;
    }

    void RespawnAtLastCheckpoint()
    {
        Freeze(false);

        if (lastCheckpointReached != null
            && lastCheckpointReached.respawnPoint != null)
        {
            transform.position =
                lastCheckpointReached.respawnPoint.transform.position;
        }
        else
        {
            RestartAtStart();
        }
    }

    private void Freeze(bool isKinematic)
    {        
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.isKinematic = isKinematic;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TagCheckpoint))
        {
            var cp = other.transform.GetComponent<Checkpoint>();
            lastCheckpointReached = cp;

            if (cp.checkpointType == Checkpoint.CheckpointType.End)
            {
                Freeze(true);
                endedLevel = true;
                OnPlayerReachedEnd();
            }
        }

        if (other.CompareTag(Constants.TagDeath))
        {
            RespawnAtLastCheckpoint();
        }
    }

    #endregion
}
