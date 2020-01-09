using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{

    public const string ApiUrl = "https://rollwithfriendsapp.azurewebsites.net";
    public const string UnityCustomTokenAPI = "245gjyu897hy8245gt25bgt76y89hu45gy8701012020";

    #region SceneNames

    public const string SceneNameGame = "_Game";    
    public const string SceneNameLevelSelection = "LevelSelection";    
    #endregion

    #region Tags

    public const string TagPlayer = "Player";
    public const string TagCheckpoint = "Checkpoint";

    public const string TagDeath = "Death";

    #endregion

    #region Buttons
    public const string ButtonResetLevel = "ResetLevel";
    public const string ButtonResetCheckpoint = "ResetCheckpoint";

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

    // Hihghscore
    public const string ApiServiceHighscoreGetBestForLevel = "/highscore/GetBestScoreForLevel/{0}";
    public const string ApiServiceHighscoreCreate = "/highscore/create";
    
    #endregion
}
