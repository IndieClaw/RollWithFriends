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

[SerializeField] private CheckpointType checkpointType;
[SerializeField] private Checkpoint previousCheckpoint;

[SerializeField] private Checkpoint nextCheckpoint;

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
	
	#endregion
}
