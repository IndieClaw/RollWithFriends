using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public const float DefaultRoundTimeSeconds = 10;
    public const string GameVersion = "v2.0";
    public const string ApiUrl = "https://rollwithfriendsapp.azurewebsites.net";
    public const string UnityCustomTokenAPI = "245gjyu897hy8245gt25bgt76y89hu45gy8701012020";

    #region SceneNames

    public const string SceneNameGame = "_Game";
    public const string SceneNameLevelSelection = "LevelSelection";
    public const string SceneNameMultiplayerLobby = "MultiplayerLobby";
    #endregion

    #region Tags

    public const string TagPlayer = "Player";
    public const string TagCheckpoint = "Checkpoint";

    public const string TagDeath = "Death";

    #endregion

    #region Buttons
    public const string ButtonResetLevel = "ResetLevel";
    public const string ButtonResetCheckpoint = "ResetCheckpoint";
    public const string ButtonShowScoreList = "ShowScoreList";

    #endregion

    #region Regex
    public const string RegexUserName = "(^[a-zA-Z0-9]{3,16})$";
    #endregion

    #region PlayerPrefs
    public const string PlayerPrefKeyUser = "User";

    #endregion

    #region API URL CALLS
    // USER
    public const string ApiServiceUserCreate = "/user/create";
    public const string ApiServiceUserDoesUserExist = "/user/DoesUserNameAlreadyExist/{0}";

    // Hihghscore
    public const string ApiServiceHighscoreGetBestForLevel = "/highscore/GetBestScoreForLevel/{0}";
    public const string ApiServiceHighscoreCreate = "/highscore/create";

    // PHOTON - Mutliplayer
    public const string PlayerPrefabName = "PlayerPrefab";
    public const string PlayerListItemPrefabName = "PlayerListItem";

    public static string[] MultiplayerLevelsArray = new string[]{
        "M1","M2","M3","M4"
        };
    #endregion
}
