using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class NetworkConnectionManager : MonoBehaviourPunCallbacks
{
	#region Fields and properties
    [SerializeField] Button connectToMasterButton;
    [SerializeField] Button joinRoomButton;

    bool isConnectingToMaster;

    bool isJoiningRoom;

	
	#endregion

    #region Public methods
	public void OnClickConnectToMaster()
    {
        PhotonNetwork.OfflineMode = false;

        if (!PlayerPrefs.HasKey(Constants.PlayerPrefKeyUser))
        {            
            Debug.LogError("No player found!");
            return;
        }

        PhotonNetwork.NickName = PlayerPrefs.GetString(Constants.PlayerPrefKeyUser);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = Constants.GameVersion;

        isConnectingToMaster = true;
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        isConnectingToMaster = false;
        isJoiningRoom = false;
        Debug.Log(cause);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        isConnectingToMaster = true;
        Debug.Log("Connected to master");
    }
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
