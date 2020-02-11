using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbySettingsController : MonoBehaviourPunCallbacks
{
    #region Fields and properties

    [SerializeField] RectTransform playerListContent;
    [SerializeField] Button playButton;

    [SerializeField] public TMP_Dropdown levelSelectionDropdown;



    [HideInInspector]
    public float roundTimeValueMinutes; // Default is 3 minutes

    #endregion

    #region Public methods
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        UpdatePlayerList();
        if (PhotonNetwork.IsMasterClient)
        {
            playButton.interactable = true;
        }

    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        UpdatePlayerList();
    }

    public void OnRoundTimeInputChange(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            roundTimeValueMinutes = int.Parse(value);
        }
    }

    #endregion


    #region Private methods	

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (LobbyManager.instance.RoomSettings != null)
        {
            UpdatePlayerList();
            if (PhotonNetwork.IsMasterClient)
            {
                playButton.interactable = true;
            }
        }
    }

    void Update()
    {
        
    }


    private void UpdatePlayerList()
    {
        if (PhotonNetwork.CurrentRoom.Players.Any())
        {
            var previousElementPosition = Vector2.zero;
            foreach (var p in PhotonNetwork.CurrentRoom.Players)
            {
                var obj = PhotonNetwork.Instantiate(
                           Constants.PlayerListItemPrefabName,
                           playerListContent.transform.position,
                           Quaternion.identity);

                obj.transform.SetParent(playerListContent.transform);

                previousElementPosition =
                    obj.GetComponent<RectTransform>().anchoredPosition
                    = Vector2.zero + previousElementPosition;
                previousElementPosition += new Vector2(0, -50); // 50 is the item height

                var nameToShow = PhotonNetwork.LocalPlayer.NickName == p.Value.NickName
                    ? p.Value.NickName + " (you)"
                    : p.Value.NickName;

                obj.GetComponentInChildren<TextMeshProUGUI>().text = nameToShow;
            }
        }
    }

    #endregion
}
