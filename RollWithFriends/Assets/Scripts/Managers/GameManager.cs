using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields and properties
    public static GameManager instance;

    public readonly HttpClient client = new HttpClient(); 

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

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    void Update()
    {

    }

    #endregion
}
