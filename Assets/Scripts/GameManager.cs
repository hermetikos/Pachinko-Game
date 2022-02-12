using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI ballCountUI;
    [SerializeField] private int ballCount = 20;
    private int ballsInNests = 0;

    private void OnEnable() {
        Counter.UpdateBallCount += this.UpdateBallCount;
    }
    // Update is called once per frame
    void Update()
    {
        // update the UI        
    }

    
    private void UpdateBallCount(int addend)
    {
        ballCount += addend;
        ballCountUI.SetText("Ball Count: " + ballCount);
        Debug.Log("Ball Count: " + ballCount);
    }
}
