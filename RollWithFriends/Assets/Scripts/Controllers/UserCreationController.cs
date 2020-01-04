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
    [SerializeField] GameObject welcomeWindow;

    [SerializeField] TMP_InputField userNameTextMesh;

    [SerializeField] TextMeshProUGUI errorMessageTextMesh;
    [SerializeField] TextMeshProUGUI welcomeTextMesh;

    
    private static readonly HttpClient _client = new HttpClient();
    #endregion

    #region Public methods
    public void SubmitUserName()
    {
        errorMessage.SetActive(false);

        var isMatch = Regex.IsMatch(userNameTextMesh.text, Constants.RegexUserName);

        if (isMatch)
        {
            PostUser();            
            
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
            try
            {
                var user = new User(userNameTextMesh.text, Constants.UnityCustomTokenAPI);
                string userJson = JsonConvert.SerializeObject(user);
                var content = new StringContent(userJson.ToString(), Encoding.UTF8, "application/json");

                var response = _client.PostAsync(
                    $"{Constants.ApiUrl + Constants.ApiServiceUserCreate}",
                    content)
                    .Result;

                response.EnsureSuccessStatusCode();

                PlayerPrefs.SetString(Constants.PlayerPrefKeyUser, userNameTextMesh.text);
                StartCoroutine(WelcomeUserRoutine(userNameTextMesh.text));

            }
            catch (System.Exception)
            {
                ShowErrorMessage("That user already exists");
            }
        }
    }

    #endregion


    #region Private methods	

    void Start()
    {
        PlayerPrefs.DeleteKey(Constants.PlayerPrefKeyUser);

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

    IEnumerator WelcomeUserRoutine(string userName)
    {
        welcomeWindow.SetActive(true);
        welcomeTextMesh.text =  "Welcome " + userName + "!";

        yield return new WaitForSeconds(2f);

        welcomeWindow.SetActive(false);
        createUserCanvas.SetActive(false);
    }
    #endregion
}
