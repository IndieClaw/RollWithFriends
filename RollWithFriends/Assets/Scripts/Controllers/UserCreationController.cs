using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Models;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class UserCreationController : MonoBehaviour
{
    #region Fields and properties
    [SerializeField] GameObject createUserCanvas;
    [SerializeField] GameObject errorMessage;
    [SerializeField] GameObject confirmationWindow;
    [SerializeField] TMP_InputField userNameTextMesh;

    [SerializeField] TextMeshProUGUI errorMessageTextMesh;
    private static readonly HttpClient _client = new HttpClient();
    #endregion

    #region Public methods
    public void SubmitUserName()
    {
        errorMessage.SetActive(false);

        var isMatch = Regex.IsMatch(userNameTextMesh.text, Constants.RegexUserName);

        if (isMatch)
        {
            try
            {   
                var serviceUrl = string.Format(Constants.ApiServiceUserDoesUserExist, userNameTextMesh.text);

                var doesUserAlreadyExist = bool.Parse(
                    _client.GetStringAsync($"{Constants.ApiUrl + serviceUrl}").Result);

                if (doesUserAlreadyExist)
                {
                    throw (new System.Exception("That user already exists"));
                }
                else
                {
                    createUserCanvas.SetActive(false);

                    // Local function
                    PostUser();
                }
            }
            catch (System.Exception e)
            {
                // If user already exists or something wene wrong throw here                
                ShowErrorMessage(e.Message);
                throw;
            }
        }
        else
        {
            ShowErrorMessage("- invalid username -");
        }

        /// <summary>
        /// Creates a user on the database and sets the playerprefs key
        /// </summary>
        void PostUser()
        {
            var user = new User(userNameTextMesh.text, Constants.UnityCustomTokenAPI);
            string userJson = JsonConvert.SerializeObject(user);
            var content = new StringContent(userJson.ToString(), Encoding.UTF8, "application/json");

            _client.PostAsync($"{Constants.ApiUrl + Constants.ApiServiceUserCreate}", content);
            PlayerPrefs.SetString(Constants.PlayerPrefKeyUser, userNameTextMesh.text);
        }
    }

    #endregion


    #region Private methods	

    void Start()
    {
        //PlayerPrefs.DeleteKey(Constants.PlayerPrefKeyUser);

        if (!PlayerPrefs.HasKey(Constants.PlayerPrefKeyUser))
        {
            createUserCanvas.SetActive(true);
        }
    }

    void Update()
    {

    }

    void ShowErrorMessage(string message)
    {
        errorMessage.SetActive(true);
        errorMessageTextMesh.text = message;
    }

    #endregion
}
