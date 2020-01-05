using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBannerController : MonoBehaviour
{
    #region Fields and properties
    [SerializeField] string levelName;
    [SerializeField] string sceneNameToLoad;

    [SerializeField] Image levelImage;
    [SerializeField] Sprite levelBannerImage;

    [SerializeField] TextMeshProUGUI personalBestTimeTextMesh;

    [SerializeField] TextMeshProUGUI firstRankTimeTextMesh;

    #endregion

    #region Public methods

    #endregion


    #region Private methods	
    void SetPersonalBest()
    {
        personalBestTimeTextMesh.text = 
            PlayerPrefs.GetFloat(sceneNameToLoad).ToString();
    }

    void SetFirstRankTime()
    {
        // TODO JS: call HighscoreService here.
        firstRankTimeTextMesh.text = "0.00";

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
