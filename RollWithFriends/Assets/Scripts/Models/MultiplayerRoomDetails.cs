using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerRoomDetails
{
    #region Fields and properties
    public int RoomPlayerCount { get; set; }

    public string LevelName { get; set; }

    public string LevelCodeName { get; set; }

    public float RoundTimeSeconds { get; set; }

    #endregion

    #region Public methods
    public MultiplayerRoomDetails()
    {

    }

    public MultiplayerRoomDetails(
        int roomPlayerCount,
        string levelName,
        string levelCodeName,
        float roundTimeSeconds)
    {
        RoomPlayerCount = roomPlayerCount;
        LevelName = levelName;
        LevelCodeName = levelCodeName;
        RoundTimeSeconds = roundTimeSeconds;
    }

    #endregion


    #region Private methods	


    #endregion
}
