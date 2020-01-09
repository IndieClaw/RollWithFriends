﻿using System.Collections;
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
                HighscoreService.CreateHighscoreForCurrentUser(levelCodeName, time, GameManager.instance.client);
            }
            catch (System.Exception ex)
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
