using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    GameObject player1;
    GameObject player2;

    public int scoreTarget;
    public int player1score;
    public int player2score;
    public int roundCount;

    public List<GameObject> activeLevels;
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public GameObject currentLevel;
    public int currentLevelIndex = 0;

    public bool hasGameStarted = false;
    public bool gameIsPaused = false;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        currentLevel = Instantiate(activeLevels[currentLevelIndex]);
    }

    private void Start()
    {
        RestartGame();
    }

    public void RoundEnd(PlayerController playerDied)
    {
       
        if (playerDied.playerNumber == PlayerControllerType.Player1)
        {
            player2score++;
        }

        if (playerDied.playerNumber == PlayerControllerType.Player2)
        {
            player1score++;
        }

        if (player1score > scoreTarget || player2score > scoreTarget)
        {
            UIManager.Instance.DisplayEndGame();
            return;
        }

        roundCount++;
        hasGameStarted = false;
        UIManager.Instance.DisplayEndRound();
    }

    public void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        gameIsPaused = false;
        Time.timeScale = 1;
    }
    public void NextRound()
    {
        hasGameStarted = true;
        SpawnPlayers();
    }

    public void RestartRound()
    {
        SpawnPlayers();
    }
    public void RestartGame()
    {
        player1score = 0;
        player2score = 0;
        roundCount = 0;
        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        if (player1 != null)
        {
            Destroy(player1);
        }
        player1 = Instantiate(player1Prefab, currentLevel.GetComponent<Level>().player1Spawn.position, Quaternion.identity);

        if (player2 != null)
        {
            Destroy(player2);
        }
        player2 = Instantiate(player2Prefab, currentLevel.GetComponent<Level>().player2Spawn.position, Quaternion.identity);

        hasGameStarted = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //UIPopup to reload round / start from the begginging
        }


    }
}

public enum PortalType
{
    Horizontal, Vertical
}

public enum PlayerControllerType
{
    Player1, Player2, Player3, Player4
}
