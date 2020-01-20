using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class LobbyController : MonoBehaviourPunCallbacks
{
	#region Fields and properties
    [SerializeField] RectTransform playerListContent;    
	
	#endregion

    #region Public methods
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Room name: " + PhotonNetwork.CurrentRoom.Name);

        var obj = PhotonNetwork.Instantiate(
            Constants.PlayerListItemPrefabName,
            playerListContent.transform.position,
            Quaternion.identity);

        obj.transform.SetParent(playerListContent.transform);

        obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //obj.GetComponent<TextMeshProUGUI>().text = "batatas";
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
