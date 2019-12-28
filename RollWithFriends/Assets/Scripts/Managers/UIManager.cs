using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Fields and properties
    public static UIManager instance;


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
        //PlayerController.OnPlayerReachedEnd += OnPlayerReachedEnd;
    }

    private void UnSubscribeEvents()
    {
        //PlayerController.OnPlayerReachedEnd -= OnPlayerReachedEnd;
    }

    #endregion
}
