using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Models;
using Newtonsoft.Json;
using UnityEngine;

public static class HighscoreService
{
    #region Fields and properties

    #endregion

    #region Public methods
    public static Highscore GetBestScoreForLevel(string levelName, HttpClient client)
    {
        try
        {
            var url = string.Format(Constants.ApiServiceHighscoreGetBestForLevel, levelName, 1);
            var data = client.GetStringAsync($"{Constants.ApiUrl + url}").Result;

            return JsonConvert.DeserializeObject<Highscore>(data);

        }
        catch (System.Exception)
        {
            throw;
        }
    }
    #endregion


    #region Private methods	



    #endregion
}
