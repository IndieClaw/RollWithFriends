using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class UserCreationController : MonoBehaviour
{
	#region Fields and properties
	[SerializeField] GameObject createUserCanvas;
    [SerializeField] GameObject errorMessage;
    [SerializeField] GameObject confirmationWindow;
    [SerializeField] TMP_InputField userNameTextMesh;
	#endregion

    #region Public methods
    public void SubmitUserName()
    {   
        errorMessage.SetActive(false);
        var isMatch = Regex.IsMatch(userNameTextMesh.text, Constants.RegexUserName);

        if (isMatch)
        {
            createUserCanvas.SetActive(false);            
        }
        else
        {
            ShowErrorMessage();
        }
    }
	
	#endregion
	
	
	#region Private methods	
	
    void Start()
    {
        if(!PlayerPrefs.HasKey(Constants.PlayerPrefKeyUser))
        {
            createUserCanvas.SetActive(true);
        }
    }
    
    void Update()
    {
        
    }

    void ShowErrorMessage()
    {
        errorMessage.SetActive(true);
    }
	
	#endregion
}
