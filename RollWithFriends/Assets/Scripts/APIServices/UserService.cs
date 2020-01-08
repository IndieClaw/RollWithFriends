using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Models;
using Newtonsoft.Json;
using UnityEngine;

public static class UserService
{
    #region Fields and properties

    #endregion

    #region Public methods
    public static void CreateUser(string userName, HttpClient client)
    {
        try
        {
            var user = new User(userName, Constants.UnityCustomTokenAPI);
            string userJson = JsonConvert.SerializeObject(user);
            var content = new StringContent(userJson.ToString(), Encoding.UTF8, "application/json");

            var response = client.PostAsync(
                $"{Constants.ApiUrl + Constants.ApiServiceUserCreate}",
                content)
                .Result;

            response.EnsureSuccessStatusCode();

            PlayerPrefs.SetString(Constants.PlayerPrefKeyUser, userName);
        }
        catch (System.Exception)
        {
            throw new System.Exception("That user already exists");
        }
    }
    #endregion


    #region Private methods	


    #endregion
}
