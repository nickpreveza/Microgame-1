using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameObject fader;
    [SerializeField] GameObject endGame;
    [SerializeField] GameObject endRound;
    [SerializeField] GameObject pauseMenu;

    [SerializeField] TextMeshProUGUI player1Score;
    [SerializeField] TextMeshProUGUI player2Score;
    [SerializeField] TextMeshProUGUI playerWon;
    [SerializeField] TextMeshProUGUI currentRound;
    [SerializeField] TextMeshProUGUI explanation;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        CloseUIPanels();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.gameIsPaused)
            {
                pauseMenu.SetActive(false);
                fader.SetActive(false);
                GameManager.Instance.UnPause();
            }
            else
            {
                pauseMenu.SetActive(true);
                fader.SetActive(true);
                GameManager.Instance.Pause();
            }
           

        }

        if (endRound.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextRound();
            }
        }
    }

    public void CloseUIPanels()
    {
        endGame.SetActive(false);
        endRound.SetActive(false);
        fader.SetActive(false);
        pauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        GameManager.Instance?.UnPause();
    }
    public void DisplayEndRound()
    {
        GameManager.Instance.Pause();
        player1Score.text = GameManager.Instance.player1score.ToString();
        player2Score.text = GameManager.Instance.player2score.ToString();
        currentRound.text = "ROUND " + GameManager.Instance.roundCount.ToString();
        explanation.text = "Best to " + GameManager.Instance.scoreTarget.ToString() + " wins";
        fader.SetActive(true);
        endRound.SetActive(true);
    }

    public void NextRound()
    {
        GameManager.Instance.NextRound();
        CloseUIPanels();

    }

    public void Restart()
    {
        GameManager.Instance.RestartGame();
        CloseUIPanels();
        
    }

    public void RestartRound()
    {
        GameManager.Instance.RestartRound();
        CloseUIPanels();
    }

    public void DisplayEndGame()
    {
        GameManager.Instance.Pause();
        if (GameManager.Instance.player1score > GameManager.Instance.player2score)
        {
            playerWon.text = "Player 1 won the game!";
        }
        else
        {
            playerWon.text = "Player 2 won the game!";
        }
        fader.SetActive(true);
        endGame.SetActive(true);
    }
}
