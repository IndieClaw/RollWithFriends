using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    #region Fields and properties
    public static LobbyManager instance;

    public MultiplayerRoomSettings RoomSettings { get; set; }

    #endregion

    #region Public methods
    public void SetMultiplayerRoomDetails(MultiplayerRoomSettings newRoomSettings)
    {        
        RoomSettings = newRoomSettings;
    }
    #endregion


    #region Private methods	
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {

    }

    void Update()
    {

    }

    #endregion
}
