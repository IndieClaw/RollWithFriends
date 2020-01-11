using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreController : MonoBehaviour
{
    #region Fields and properties

    #endregion

    #region Public methods
    public void CreateHighscoreForCurrentUser(string levelName, string levelCodeName, float time)
    {
        var personalScore = PlayerPrefs.GetFloat(levelCodeName);

        if (personalScore == 0 || time < personalScore)
        {
            try
            {

                float truncated = (float)(Math.Truncate((double)time * 100.0) / 100.0);

                float roundedTime = (float)(Math.Round((double)time, 2));


                HighscoreService.CreateHighscoreForCurrentUser(levelCodeName, roundedTime, GameManager.instance.client);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
    #endregion


    #region Private methods	

    void Start()
    {
        SubscribeEvents();
    }

    void Update()
    {

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    void SubscribeEvents()
    {
        LevelManager.OnLevelEnded += CreateHighscoreForCurrentUser;
    }

    void UnsubscribeEvents()
    {
        LevelManager.OnLevelEnded -= CreateHighscoreForCurrentUser;
    }

    #endregion
}
