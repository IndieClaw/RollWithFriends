using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Fields and properties
    public static event Action OnLevelStarted = delegate { };
    public static event Action<float> OnLevelEnded = delegate { };

    [SerializeField] string levelName;

    byte startingCountdownTime = 3;
    WaitForSeconds waitForSecondCountDown;

    [SerializeField] GameObject playerPrefab;

    Vector3 startingCheckpoint;

    bool canIncrementLevelTimer;

    public bool isInDevelopmentMode = false;

    float levelTimer = 0f;

    [SerializeField] TextMeshProUGUI timerTextMesh;

    #endregion

    #region Public methods

    public void InitializeLevel()
    {
        StartCountdownTimer();
        InstantiatePlayer();
    }

    #endregion


    #region Private methods	

    void Start()
    {
        SubscribeEvents();

        waitForSecondCountDown = new WaitForSeconds(0.35f);

        startingCheckpoint = GameObject.FindObjectsOfType<Checkpoint>()
            .Where(c =>
                c.checkpointType == Checkpoint.CheckpointType.Start)
                .FirstOrDefault().transform.position;

        InitializeLevel();

    }

    void Update()
    {
        if (canIncrementLevelTimer)
        {
            levelTimer += Time.deltaTime;

            if (timerTextMesh != null)
            {
                timerTextMesh.text = levelTimer.ToString("F2");
            }                
        }
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
        PlayerController.OnPlayerReachedEnd += OnPlayerReachedEnd;
        PlayerController.OnPlayerResetLevel += ResetLevel;
    }

    private void UnSubscribeEvents()
    {
        PlayerController.OnPlayerReachedEnd -= OnPlayerReachedEnd;
        PlayerController.OnPlayerResetLevel -= ResetLevel;
    }

    void InstantiatePlayer()
    {
        Instantiate(
            original: playerPrefab,
             position: startingCheckpoint,
              rotation: Quaternion.identity);
    }

    void OnPlayerReachedEnd(PlayerController player)
    {
        canIncrementLevelTimer = false;
        OnLevelEnded(levelTimer);
        
    }

    void ResetLevel()
    {
        levelTimer = 0;
        timerTextMesh.text = "0.00";

        canIncrementLevelTimer = false;
        StartCountdownTimer();
    }

    void LevelStarted()
    {
        canIncrementLevelTimer = true;
    }    

    void StartCountdownTimer()
    {
        StartCoroutine(StartCountdownTimerCoroutine());
    }

    IEnumerator StartCountdownTimerCoroutine()
    {
        if (isInDevelopmentMode) // TODO JS: remove in final build
        {
            yield return new WaitForSeconds(0.1f);
            OnLevelStarted();
            LevelStarted();
            yield return null;
        }
        else
        {
            for (int i = startingCountdownTime; i >= 0; i--)
            {
                print(i);
                if (i == 0)
                {
                    print("GO!");
                    OnLevelStarted();
                    LevelStarted();
                    yield return null;
                }

                yield return waitForSecondCountDown;

            }
        }
    }

    #endregion
}
