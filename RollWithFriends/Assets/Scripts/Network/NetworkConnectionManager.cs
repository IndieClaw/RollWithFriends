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
    [SerializeField] Button joinRoomButton;

    [SerializeField] TMP_InputField roomNameInputField;

    [SerializeField] GameObject lobbyWindow;

    [SerializeField] TextMeshProUGUI roomNameTextMesh;
    [SerializeField] TextMeshProUGUI connectingLabelTextMesh;
    [SerializeField] TMP_Dropdown levelSelectionDropdown;

    #endregion

    #region Public methods
    public void LoadMultiplayerLevel()
    {
        var levelNameToLoad = Constants.MultiplayerLevelsArray[levelSelectionDropdown.value];
        //SceneManager.LoadScene(levelNameToLoad);
        StartCoroutine(LoadLevelAsyncRoutine(levelNameToLoad));
    }

    public void ConnectToMaster()
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
        connectingLabelTextMesh.text = "Connected";
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Room name: " + PhotonNetwork.CurrentRoom.Name);
        lobbyWindow.SetActive(true);
        SetRoomDetailsData();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // TODO JS: update player list
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
        connectingLabelTextMesh.gameObject.SetActive(true);
        ConnectToMaster();
    }

    void Update()
    {

    }

    private void SetRoomDetailsData()
    {
        roomNameTextMesh.text = PhotonNetwork.CurrentRoom.Name;
    }

    IEnumerator LoadLevelAsyncRoutine(string levelName)
    {
        yield return null;

        AsyncOperation levelAsync = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        levelAsync.allowSceneActivation = false;

        while (!levelAsync.isDone)
        {
            if (levelAsync.progress >= 0.9f)
            {
                levelAsync.allowSceneActivation = true;

            }

            yield return null;
        }

        AsyncOperation gameAsync = SceneManager.LoadSceneAsync(Constants.SceneNameGame, LoadSceneMode.Additive);
        gameAsync.allowSceneActivation = false;

        while (!gameAsync.isDone)
        {
            if (gameAsync.progress >= 0.9f)
            {
                gameAsync.allowSceneActivation = true;
            }
            yield return null;
        }

        var gameScene = SceneManager.GetSceneByName(Constants.SceneNameGame);
        SceneManager.SetActiveScene(gameScene);
        UpdateLevelManager(levelName);
        SceneManager.UnloadSceneAsync(Constants.SceneNameMultiplayerLobby);

        yield return null;
    }

    void UpdateLevelManager(string levelName)
    {
        var levelManager = FindObjectOfType<LevelManager>();

        if (levelManager != null)
        {
            levelManager.SetLevelNameData(levelName, levelName); // TODO JS: level name and code name
        }
    }
    #endregion
}
