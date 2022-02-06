using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plunger : MonoBehaviour
{
    private float power;
    private float powerRate;
    public float minPower = 0.0f;
    public float maxPower = 100.0f;
    public float maxTorque = 1.0f;
    public Slider powerSlider;

    private bool ballReady = false;
    List<Rigidbody> ballList = new List<Rigidbody>();

    void Start()
    {
        powerSlider.minValue = minPower;
        powerSlider.maxValue = maxPower;
        powerRate = maxPower / 2.0f;
        powerSlider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ballReady)
        {
            powerSlider.gameObject.SetActive(true);            
        }
        else
        {
            powerSlider.gameObject.SetActive(false);            
        }


        powerSlider.value = power;
        if (ballList.Count > 0)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                ballReady = true;
                if(power <= maxPower)
                {
                    power += powerRate * Time.deltaTime;
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                foreach (Rigidbody r in ballList)
                {
                    r.AddForce(power * Vector3.up, ForceMode.Impulse);
                    r.AddTorque(r.transform.right * Random.Range(-maxTorque, maxTorque), ForceMode.Impulse);
                }
                ballReady = false;
                power = minPower;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Ball"))
        {
            ballList.Add(other.gameObject.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Ball"))
        {
            ballList.Remove(other.gameObject.GetComponent<Rigidbody>());
            power = minPower;
        }
    }
}
