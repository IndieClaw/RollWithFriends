using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    #region Fields and properties

    [SerializeField] private Rigidbody rigidBody;

    private Checkpoint lastCheckpointReached;

    public event Action OnPlayerReachedEnd = delegate { };

    public event Action OnPlayerResetLevel = delegate { };

    [SerializeField] GameObject cameraHolder;
    bool endedLevel;
    bool canResetPlayer = true;

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
            Destroy(cameraHolder);
            return;
        }

        SubscriveEvents();
    }

    void Update()
    {
        if (!doIHaveControllOverThisPlayer)
            return;

        if (canResetPlayer)
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

    }

    void SubscriveEvents()
    {
        LevelManager.OnLevelWasReset += RestartAtStart;
        LevelManager.OnMultiplayerRoundFinish += OnMultiplayerRoundFinish;
    }

    void UnsubscribeEvents()
    {
        LevelManager.OnLevelWasReset -= RestartAtStart;
        LevelManager.OnMultiplayerRoundFinish -= OnMultiplayerRoundFinish;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void OnMultiplayerRoundFinish()
    {
        canResetPlayer = false;
        Freeze(true);
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
