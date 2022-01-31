using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxHoldTime = 2.0f;
    
    [SerializeField] private float maxLaunchForce = 60.0f;
    [SerializeField] private float maxTorque = 0.1f;

    [SerializeField] private GameObject ballPrefab;
    
    [SerializeField] private GameObject ballSpawn;
    private System.DateTime currentTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.LeftControl)) {
        //     currentTime = System.DateTime.Now;
        // }

        // if(currentTime.Millisecond > 0.0f && Input.GetKeyUp(KeyCode.LeftControl)) {
        //     SpawnBall();            
        // }
    }

    private void SpawnBall() {
        // calculate launch force
        float holdTime = (System.DateTime.Now - currentTime).Milliseconds;
        holdTime = Mathf.Clamp(holdTime / 1000, 0.0f, maxHoldTime);
        float launchForce = (holdTime / maxHoldTime) * maxLaunchForce;
        
        // spawn a new ball and apply a force and torque
        GameObject newBall = Instantiate(
            ballPrefab,
            ballSpawn.transform.position,
            ballPrefab.transform.rotation
        );

        // calculate random torque
        Vector3 torque = newBall.transform.right * UnityEngine.Random.Range(-maxTorque, maxTorque);

        newBall.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, launchForce, 0.0f), ForceMode.Impulse);
        newBall.GetComponent<Rigidbody>().AddTorque(torque, ForceMode.Impulse);
    }
}
