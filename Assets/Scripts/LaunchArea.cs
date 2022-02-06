using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchArea : MonoBehaviour
{
    private bool launchAreaClear = true;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Ball"))
        {
            launchAreaClear = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Ball"))
        {
            launchAreaClear = true;
        }
    }

    public bool isLaunchAreaClear()
    {
        return launchAreaClear;
    }
}
