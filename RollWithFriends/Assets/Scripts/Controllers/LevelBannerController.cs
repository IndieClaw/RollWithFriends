using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelBannerController : MonoBehaviour
{
    #region Fields and properties
    [SerializeField] string levelName;
    [SerializeField] string levelCodeName;
    [SerializeField] string sceneNameToLoad;

    [SerializeField] Image levelImage;
    [SerializeField] Sprite levelBannerImage;
    [SerializeField] TextMeshProUGUI levelNameTextMesh;

    [SerializeField] TextMeshProUGUI personalBestTimeTextMesh;

    [SerializeField] TextMeshProUGUI firstRankUserTextMesh;
    [SerializeField] TextMeshProUGUI firstRankTimeTextMesh;

    #endregion

    #region Public methods
    public void LoadLevelAsync()
    {
        StartCoroutine(LoadLevelAsyncRoutine());

    }
    #endregion


    #region Private methods	

    void Start()
    {
        SetLevelName();
        SetPersonalBest();
        SetFirstRankTime();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.UnloadSceneAsync("LevelSelection");
        }
    }

    void SetPersonalBest()
    {
        personalBestTimeTextMesh.text =
            PlayerPrefs.GetFloat(sceneNameToLoad).ToString();
    }

    void SetFirstRankTime()
    {        
        var bestScore = HighscoreService.GetBestScoreForLevel(levelCodeName, GameManager.instance.client);

        if (bestScore != null)
        {   
            firstRankUserTextMesh.text = bestScore.User.Name;
            firstRankTimeTextMesh.text = bestScore.Time.ToString();         
        }
        else
        {            
            firstRankTimeTextMesh.text = "0.00";
        }

    }

    void SetLevelName()
    {
        levelNameTextMesh.text = levelName;
    }


    IEnumerator LoadLevelAsyncRoutine()
    {
        yield return null;

        AsyncOperation levelAsync = SceneManager.LoadSceneAsync(sceneNameToLoad, LoadSceneMode.Additive);

        levelAsync.allowSceneActivation = false;

        while (!levelAsync.isDone)
        {
            if (levelAsync.progress >= 0.9f)
            {
                levelAsync.allowSceneActivation = true;

            }

            yield return null;
        }

        AsyncOperation gameAsync = SceneManager.LoadSceneAsync(Constants.SceneNameGame, LoadSceneMode.Additive);
        gameAsync.allowSceneActivation = false;

        while (!gameAsync.isDone)
        {
            if (gameAsync.progress >= 0.9f)
            {
                gameAsync.allowSceneActivation = true;
            }
            yield return null;
        }

        var gameScene = SceneManager.GetSceneByName(Constants.SceneNameGame);
        SceneManager.SetActiveScene(gameScene);
        SceneManager.UnloadSceneAsync(Constants.SceneNameLevelSelection);

        yield return null;
    }

    #endregion
}
