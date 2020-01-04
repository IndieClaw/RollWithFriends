using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBannerController : MonoBehaviour
{
    #region Fields and properties
    [SerializeField] string levelName;
    [SerializeField] string sceneToLoad;

    [SerializeField] Image levelImage;

    [SerializeField] TextMeshProUGUI personalBestTimeTextMesh;

    [SerializeField] TextMeshProUGUI firstRankTimeTextMesh;

    #endregion

    #region Public methods

    #endregion


    #region Private methods	
    void SetPersonalBest()
    {
        personalBestTimeTextMesh.text = PlayerPrefs.GetFloat("level_" + levelName).ToString();
    }

    void SetFirstRankTime()
    {
        // TODO JS: call HighscoreService here.
        firstRankTimeTextMesh.text = "0";

    }

    void Start()
    {
        SetPersonalBest();
        SetFirstRankTime();
    }

    void Update()
    {

    }

    #endregion
}
