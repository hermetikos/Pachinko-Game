using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    
    public delegate void GameEvent();
    public static event GameEvent GameOver;

    [SerializeField] private TextMeshProUGUI ballCountUI;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameOverCountdown;
    [SerializeField] private int gameoverCountdownTime = 5;
    [SerializeField] private int ballCount = 20;
    private bool runGameOverCountdown = false;
    private List<IEnumerator> countdownTimers;
    private WaitForSeconds delay = new WaitForSeconds(1);
    private void OnEnable() {
        Counter.UpdateBallCount += this.UpdateBallCount;
        PlayerController.UpdateBallCount += this.UpdateBallCount;
    }

    private void OnDisable() {
        Counter.UpdateBallCount -= this.UpdateBallCount;
        PlayerController.UpdateBallCount -= this.UpdateBallCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (ballCount <= 0
            && !runGameOverCountdown)
        {
            runGameOverCountdown = true;
            StartCoroutine(GameOverCountdown());
        }
        else if (ballCount > 0)
        {
            runGameOverCountdown = false;
            gameOverCountdown.SetActive(false);
            StopAllCoroutines();
        }
    }

    
    private void UpdateBallCount(int addend)
    {
        ballCount += addend;
        if(ballCount == 0)
        {
            ballCountUI.SetText("LAST BALL");
        }
        else
        {
            ballCountUI.SetText("Ball Count: " + ballCount);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex);
    }

    public int GetBallCount()
    {
        return ballCount;
    }

    IEnumerator GameOverCountdown()
    {
        // show the countdown UI
        gameOverCountdown.SetActive(true);
        // get the GUI object
        TextMeshProUGUI gameOverCountdownUI = gameOverCountdown.GetComponent<TextMeshProUGUI>();

        // initiate a countdown timer
        for(int countdown = gameoverCountdownTime; countdown >= 0; countdown--)
        {
            // update the ui
            gameOverCountdownUI.SetText("Game Over in: " + countdown);

            // pause the coroutine for a second
            yield return delay;
        }

        // if we run out the timer, then it's game over
        if(GameOver != null)
        {
            GameOver();
            gameOverScreen.SetActive(true);
        }        
    }
}
