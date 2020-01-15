using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    #region Fields and properties
    public enum CheckpointType
    {
        Start,
        End,
        Checkpoint
    }

    [SerializeField] public CheckpointType checkpointType;

    [SerializeField] private Checkpoint previousCheckpoint;

    [SerializeField] public MeshCollider checkpointMeshCollider;
    [SerializeField] public MeshRenderer checkpointMeshRenderer;

    [SerializeField] public GameObject respawnPoint;


    #endregion

    #region Public methods

    /// <summary>
    /// Disables the checkpoint collider so that the player cant 
    /// pass through it.
    /// </summary>
    public void DisableCheckPoint()
    {
        if(checkpointMeshCollider != null)
        {
            checkpointMeshCollider.enabled = false;
            checkpointMeshRenderer.enabled = false;
        }
        
        if(previousCheckpoint != null)
        {
            previousCheckpoint.DisableCheckPoint();
        }
    }


    #endregion


    #region Private methods	

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {       
        // We only want to deactivate checkpoints that are not "start" or "end" 
        if (checkpointType == CheckpointType.Checkpoint
            && other.CompareTag(Constants.TagPlayer))
        {
            if (checkpointMeshCollider != null)
            {                
                checkpointMeshCollider.enabled = false;
                checkpointMeshRenderer.enabled = false;
            }

            if (previousCheckpoint != null)
            {                
                previousCheckpoint.DisableCheckPoint();
            }
        }
    }
    #endregion
}
