using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class TestCall : MonoBehaviour
{
    #region Fields and properties
    private static readonly HttpClient _client = new HttpClient();

    private static readonly string _remoteUrl = "https://rollwithfriendsapp.azurewebsites.net";
    private static readonly string tokenUrl = "https://rollwithfriendsapp.azurewebsites.net/.auth/me";
    #endregion

    #region Public methods

    #endregion


    #region Private methods	

    void Start()
    {
        try
        {
            var u = GetUserTest();
            print(u.Name);
            
        }
        catch (System.Exception)
        {
            
            Debug.LogError("Somethind went wrong fetching the user.");
            throw;
        }        
    }

    void Update()
    {

    }

    User GetUserTest()
    {
        string data;

        data = _client.GetStringAsync($"{_remoteUrl}/user/1").Result;
        
        return JsonConvert.DeserializeObject<User>(data);
    }

    #endregion
}
public class BaseEntity
{
    public BaseEntity()
    {

    }

    public BaseEntity(string unityCustomTokenAPI)
    {
        UnityCustomTokenAPI = unityCustomTokenAPI;
    }

    public string Id { get; set; }

    public string UnityCustomTokenAPI { get; set; }
}

public class User : BaseEntity
{

    public string Name { get; set; }


    public User()
    {

    }

}
