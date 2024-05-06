using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject CanvasGame;
    public GameObject CanvasRestart;
    public GameObject CanvasStart;
    [Header("CanvasRestart")]
    public GameObject WinText;
    public GameObject LoseText;
    [Header("Other")]
    public AudioManager audioManager;
    public PuckScript puckScript;
    public PlayerMovement playerMovement;
    public AIScript aiScript;
    public ScoreScript scoreScript;
    public TextMeshProUGUI countDownText;

    public AIScript aIScript;
    public GameObject[] gameObjectsToDisable;



    public bool gameStarted = false;

    private void Awake()
    {
        // Disable everything at the start
        foreach (var gameObject in gameObjectsToDisable)
        {
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        CanvasGame.SetActive(false);
        CanvasStart.SetActive(true);
    }

    public void ShowRestartCanvas(bool didAIWin)
    {
        Time.timeScale = 0;

        CanvasGame.SetActive(false);
        CanvasRestart.SetActive(true);

        if (didAIWin)
        {
            WinText.SetActive(false);
            LoseText.SetActive(true);
            audioManager.PlayLostGame();
        }
        else
        {
            WinText.SetActive(true);
            LoseText.SetActive(false);
            audioManager.PlayWonGame();
        }

    }
    public void RestartGame()
    {
        CanvasRestart.SetActive(false);
        CanvasStart.SetActive(true);

        puckScript.CenterPuck();



        playerMovement.ResetPlayer();
        aiScript.ResetPosition();
        scoreScript.ResetScores();


        StartGame();


    }
    private IEnumerator SetGameStarted()
    {

        for (int i = 3; i > 0; i--)
        {
            countDownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        CanvasStart.SetActive(false);
        CanvasGame.SetActive(true);
        foreach (var gameObject in gameObjectsToDisable)
        {
            if (!gameObject.active)
            {
                gameObject.SetActive(true);
            }

        }

        Time.timeScale = 1;

    }
    public void StartGame()
    {

        StartCoroutine(SetGameStarted());
    }
}