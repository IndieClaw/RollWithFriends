using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Fields and properties
    public static event Action OnLevelStarted = delegate { };

    [SerializeField] string levelName;

    byte startingCountdownTime = 3;
    WaitForSeconds waitForOneSecond;

    [SerializeField] GameObject playerPrefab;

    Vector3 startingCheckpoint;

    public bool isInDevelopmentMode = false;

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
        waitForOneSecond = new WaitForSeconds(1);

        startingCheckpoint = GameObject.FindObjectsOfType<Checkpoint>()
            .Where(c =>
                c.checkpointType == Checkpoint.CheckpointType.Start)
                .FirstOrDefault().transform.position;

        InitializeLevel();

    }

    void Update()
    {

    }

    void InstantiatePlayer()
    {
        Instantiate(
            original: playerPrefab,
             position: startingCheckpoint,
              rotation: Quaternion.identity);
    }

    void StartCountdownTimer()
    {
        StartCoroutine(StartCountdownTimerCoroutine());
    }

    IEnumerator StartCountdownTimerCoroutine()
    {
        if (isInDevelopmentMode) // TODO JS: remove
        {
            yield return new WaitForSeconds(0.1f);
            OnLevelStarted();
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
                    yield return null;
                }

                yield return waitForOneSecond;

            }
        }
    }

    #endregion
}
