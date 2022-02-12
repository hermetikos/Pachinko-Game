using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public int multiplier = 1;

    
    public delegate void ScoreAction(int ballCount);
    public static event ScoreAction UpdateBallCount;


    private void Update() {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            // Count += multiplier;
            // Debug.Log("Count : " + Count);
            // GameManager.ModifyScore(multiplier);
            if(UpdateBallCount != null)
            {
                UpdateBallCount(multiplier);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            // Count += multiplier;
            // Debug.Log("Count : " + Count);
            if(UpdateBallCount != null)
            {
                UpdateBallCount(-multiplier);
            }
        }
    }
}
