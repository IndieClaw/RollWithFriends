using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region Fields and properties

    [SerializeField] TextMeshProUGUI finalCanvasTimer;
    [SerializeField] GameObject finalScreenCanvas;

    #endregion

    #region Public methods
 
    public void GoToLevelSelection()
    {
        SceneManager.LoadScene(Constants.SceneNameLevelSelection);
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
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
        UnSubscribeEvents();
    }

    private void OnDestroy()
    {
        UnSubscribeEvents();
    }

    private void SubscribeEvents()
    {
        LevelManager.OnLevelStarted += HideFinalScreen;
        LevelManager.OnLevelEnded += ShowFinalScreen;
    }

    private void UnSubscribeEvents()
    {
        LevelManager.OnLevelStarted -= HideFinalScreen;
        LevelManager.OnLevelEnded -= ShowFinalScreen;
    }

    void ShowFinalScreen(string levelName, string levelCodeName, float finalTime)
    {
        if(finalScreenCanvas != null)
        {
            finalScreenCanvas.SetActive(true);
        }
        finalCanvasTimer.text = finalTime.ToString("F2");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void HideFinalScreen()
    {
        if (finalScreenCanvas != null)
        {            
            finalScreenCanvas.SetActive(false);
        }
        finalCanvasTimer.text = "0.00";
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    #endregion
}
