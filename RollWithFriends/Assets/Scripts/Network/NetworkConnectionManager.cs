using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkConnectionManager : MonoBehaviourPunCallbacks
{
	#region Fields and properties
    [SerializeField] Button connectToMasterButton;
    [SerializeField] Button joinRoomButton;

    [SerializeField] TMP_InputField roomNameInputField;

    [SerializeField] GameObject lobbyWindow;

    [SerializeField] TextMeshProUGUI roomNameTextMesh;
	
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

        PhotonNetwork.ConnectUsingSettings();

    }

    public void JoinOrCreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        var roomName = roomNameInputField.text != string.Empty
             ? roomNameInputField.text
             : Guid.NewGuid().ToString();
             
        var roomOptions = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
        //PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log(cause);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to master");
        joinRoomButton.interactable = true;
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Room name: "+PhotonNetwork.CurrentRoom.Name);
        lobbyWindow.SetActive(true);
        SetRoomDetailsData();
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        
    }
	#endregion
	
	
	#region Private methods	
	
    void Start()
    {
        joinRoomButton.interactable = false;
    }
    
    void Update()
    {
        
    }

    private void SetRoomDetailsData()
    {
        roomNameTextMesh.text = PhotonNetwork.CurrentRoom.Name;
    }
	
	#endregion
}
