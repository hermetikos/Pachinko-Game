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
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private int ballCount = 20;
    private bool gameOver = false;
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
        && GameOver != null)
        {
            GameOver();
            GameOverScreen.SetActive(true);
        }
    }

    
    private void UpdateBallCount(int addend)
    {
        ballCount += addend;
        ballCountUI.SetText("Ball Count: " + ballCount);
        Debug.Log("Ball Count: " + ballCount);
    }

    public void Restart()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex);
    }
}
