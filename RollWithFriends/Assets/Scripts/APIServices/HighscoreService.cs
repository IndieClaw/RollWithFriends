using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Models;
using Newtonsoft.Json;
using UnityEngine;

public static class HighscoreService
{
    #region Fields and properties

    #endregion

    #region Public methods
    public static Highscore GetBestScoreForLevel(string levelCodeName, HttpClient client)
    {
        try
        {
            var url = string.Format(Constants.ApiServiceHighscoreGetBestForLevel, levelCodeName, 1);
            var data = client.GetStringAsync($"{Constants.ApiUrl + url}").Result;

            return JsonConvert.DeserializeObject<Highscore>(data);

        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public static void CreateHighscoreForCurrentUser(        
        string levelCodeName,
        float time,
        HttpClient client)
    {
        try
        {
            var user = new User
                (PlayerPrefs.GetString(Constants.PlayerPrefKeyUser),
                Constants.UnityCustomTokenAPI);

            var score = new Highscore(
                id: "_",
                levelName:  levelCodeName,
                time: time,
                user: user,
                unityCustomTokenAPI: Constants.UnityCustomTokenAPI
            );


            string scoreJson = JsonConvert.SerializeObject(score);
            var content = new StringContent(scoreJson.ToString(), Encoding.UTF8, "application/json");

            var response = client.PostAsync(
                $"{Constants.ApiUrl + Constants.ApiServiceHighscoreCreate}",
                content)
                .Result;

            response.EnsureSuccessStatusCode();

            PlayerPrefs.SetFloat(levelCodeName, time);
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
