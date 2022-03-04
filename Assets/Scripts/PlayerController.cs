using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public delegate void ScoreAction(int ballCount);
    public static event ScoreAction UpdateBallCount;

    [SerializeField] private float maxHoldTime = 2.0f;
    
    [SerializeField] private float maxLaunchForce = 60.0f;


    [SerializeField] private GameObject ballPrefab;
    
    [SerializeField] private GameObject ballSpawn;
    [SerializeField] private LaunchArea launchArea;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private float cameraSpeed = 10.0f;
    private CameraController cameraController;


    private GameObject newestBall;

    private System.DateTime currentTime;
    private bool gameOver = false;

    private void OnEnable() {
        GameManager.GameOver += this.GameOver;
    }
    private void OnDisable() {
        GameManager.GameOver -= this.GameOver;
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            if(Input.GetKeyDown(KeyCode.LeftControl)
                && launchArea.isLaunchAreaClear()
                && gameManager.GetBallCount() > 0
                ) {
                // first unset the active ball
                cameraController.UnsetFocus();
                // then spawn a new one
                SpawnBall();
            }
        }
    }

    private void SpawnBall() {
        
        // spawn a new ball and apply a force and torque
        newestBall = Instantiate(
            ballPrefab,
            ballSpawn.transform.position,
            ballPrefab.transform.rotation
        );
        if(UpdateBallCount != null)
        {
            UpdateBallCount(-1);
        }

        cameraController.SetFocus(newestBall);
    }

    private void GameOver()
    {
        gameOver = true;
    }

}
