using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Fields and properties
    public static UIManager instance;

    [SerializeField] TextMeshProUGUI finalCanvasTimer;
    [SerializeField] GameObject finalScreenCanvas;

    #endregion

    #region Public methods

    #endregion


    #region Private methods	
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }
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

    void ShowFinalScreen(float finalTime)
    {
        finalScreenCanvas.SetActive(true);
        finalCanvasTimer.text = finalTime.ToString("F2");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void HideFinalScreen()
    {
        finalScreenCanvas.SetActive(false);
        finalCanvasTimer.text = "0.00";
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    #endregion
}
