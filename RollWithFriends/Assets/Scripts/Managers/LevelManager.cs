using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviourPunCallbacks
{
    #region Fields and properties
    public static event Action OnLevelWasReset = delegate { };
    public static event Action OnLevelStarted = delegate { };
    public static event Action<string, string, float> OnLevelEnded = delegate { };

    string levelName;

    string levelCodeName;

    byte startingCountdownTime = 3;
    WaitForSeconds waitForSecondCountDown;

    [SerializeField] GameObject playerPrefab;

    Vector3 startingCheckpoint;

    bool canIncrementLevelTimer;

    public bool isInDevelopmentMode = false;

    float levelTimer = 0f;

    [SerializeField] TextMeshProUGUI timerTextMesh;
    [SerializeField] TextMeshProUGUI countDownTextMesh;

    bool isMultiplayer;
    float levelPlayerCount = 0;
    float loadedPlayersCount = 0;
    #endregion

    #region Public methods

    public void SetLevelNameData(
        string lvlName,
        string lvlCodeName)
    {
        levelName = lvlName;
        levelCodeName = lvlCodeName;
    }

    public void SetMultiplayer(bool isMultiplayerFlag)
    {
        isMultiplayer = isMultiplayerFlag;
    }

    public void SetRoomPlayerCount(int playerCount)
    {
        levelPlayerCount = playerCount;
    }

    public void InitializeLevel()
    {
        StartCoroutine(InitializeLelvelCoroutine());
    }

    public void ResetLevel()
    {
        OnLevelWasReset();
        levelTimer = 0;
        timerTextMesh.text = "0.00";

        // Re-enable all checkpoints
        GameObject.FindObjectsOfType<Checkpoint>()
            .Where(c =>
                c.checkpointType == Checkpoint.CheckpointType.Checkpoint)
            .ToList()
            .ForEach(c =>
                {
                    c.checkpointMeshCollider.enabled = true;
                    c.checkpointMeshRenderer.enabled = true;
                });

        canIncrementLevelTimer = false;
        countDownTextMesh.gameObject.SetActive(true);
        StartCountdownTimer();
    }


    #endregion


    #region Private methods	
    private void Awake()
    {
        
    }

    void Start()
    {
        SubscribeEvents();

        waitForSecondCountDown = new WaitForSeconds(0.35f);

        var startingCp = GameObject.FindObjectsOfType<Checkpoint>()
            .Where(c =>
                c.checkpointType == Checkpoint.CheckpointType.Start)
            .FirstOrDefault();


        if (startingCp == null)
        {
            Debug.LogError("The starting checkpoint is null, please add it to the level");
        }
        else
        {
            startingCheckpoint = startingCp.transform.position;
            InitializeLevel();
        }
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


    public override void OnDisable()
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

    IEnumerator InitializeLelvelCoroutine()
    {
        yield return new WaitForSeconds(0.1f);

        // Instantiate player
        if (isMultiplayer)
        {
            // TODO JS: random spawn points
            PhotonNetwork.Instantiate(
                Constants.PlayerPrefabName,
                startingCheckpoint,
                Quaternion.identity);

            photonView.RPC(nameof(IncrementLoadedPlayers), RpcTarget.All);
        }
        else
        {
            // In single player we start the game immediately
            Instantiate(
                original: playerPrefab,
                position: startingCheckpoint,
                rotation: Quaternion.identity);

            StartCountdownTimer();
        }
    }

    void OnPlayerReachedEnd()
    {
        canIncrementLevelTimer = false;
        OnLevelEnded(levelName, levelCodeName, levelTimer);

    }

    void LevelStarted()
    {
        countDownTextMesh.gameObject.SetActive(false);
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
                countDownTextMesh.text = i.ToString();
                if (i == 0)
                {
                    OnLevelStarted();
                    LevelStarted();
                    yield return null;
                }

                yield return waitForSecondCountDown;

            }
        }
    }

    #endregion

    #region RPC'S
    [PunRPC]
    void IncrementLoadedPlayers()
    {
        loadedPlayersCount++;
        print(loadedPlayersCount);

        if (loadedPlayersCount == levelPlayerCount)
        {
            print("can start level");
        }
    }

    #endregion
}
