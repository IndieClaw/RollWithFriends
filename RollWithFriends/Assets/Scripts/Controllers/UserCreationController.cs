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
    [SerializeField] TextMeshProUGUI welcomeExistingUserTextMesh;


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
                UserService.CreateUser(userNameTextMesh.text, GameManager.instance.client);
                StartCoroutine(WelcomeCreatedUserRoutine(userNameTextMesh.text));
            }
            catch (System.Exception ex)
            {
                ShowErrorMessage("That user already exists.");
            }
        }
        else
        {
            ShowErrorMessage("- invalid username -");
        }
    }

    #endregion


    #region Private methods	

    void Start()
    {
        //PlayerPrefs.DeleteKey(Constants.PlayerPrefKeyUser);
        ///PlayerPrefs.SetString(Constants.PlayerPrefKeyUser, "master");
        if (!PlayerPrefs.HasKey(Constants.PlayerPrefKeyUser))
        {
            createUserCanvas.SetActive(true);
        }
        else
        {
            var userExistsInDB = UserService.DoesUserExist(
                PlayerPrefs.GetString(Constants.PlayerPrefKeyUser),
                GameManager.instance.client);

            if (!userExistsInDB)
            {
                createUserCanvas.SetActive(true);
                PlayerPrefs.DeleteKey(Constants.PlayerPrefKeyUser);
            }
            else
            {
                WelcomeExistingUser();
            }
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

    IEnumerator WelcomeCreatedUserRoutine(string userName)
    {
        welcomeWindow.SetActive(true);
        welcomeTextMesh.text = "Welcome " + userName + "!";

        yield return new WaitForSeconds(2f);
        WelcomeExistingUser();
        welcomeWindow.SetActive(false);
        createUserCanvas.SetActive(false);
    }

    void WelcomeExistingUser()
    {
        welcomeExistingUserTextMesh.text = "Welcome " + PlayerPrefs.GetString(Constants.PlayerPrefKeyUser);
    }
    #endregion
}
