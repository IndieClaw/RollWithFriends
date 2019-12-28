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

    [SerializeField] private GameObject checkpointCollider;

    [SerializeField] public GameObject respawnPoint;


    #endregion

    #region Public methods

    /// <summary>
    /// Disables the checkpoint collider so that the player cant 
    /// pass through it.
    /// </summary>
    public void DisableCheckPoint()
    {
        checkpointCollider.SetActive(false);
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
        if (other.CompareTag(Constants.TagPlayer))
        {            
            if (previousCheckpoint != null)
            {                
                previousCheckpoint.DisableCheckPoint();
            }
        }
    }
    #endregion
}
