using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerRoomSettings
{
    #region Fields and properties
    public int RoomPlayerCount { get; set; }

    public string RoomName { get; set; }

    public int RoomMasterClientId { get; set; }

    public string LevelName { get; set; }

    public string LevelCodeName { get; set; }

    public float RoundTimeSeconds { get; set; }

    #endregion

    #region Public methods
    public MultiplayerRoomSettings()
    {

    }

    public MultiplayerRoomSettings(
        int roomPlayerCount,
        string roomName,
        int roomMasterClientId,
        string levelName,
        string levelCodeName,
        float roundTimeSeconds)
    {
        RoomPlayerCount = roomPlayerCount;
        RoomName = roomName;
        RoomMasterClientId = roomMasterClientId;
        LevelName = levelName;
        LevelCodeName = levelCodeName;
        RoundTimeSeconds = roundTimeSeconds;
    }

    #endregion


    #region Private methods	


    #endregion
}
