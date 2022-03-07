using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public float manualMoveSpeed = 3.0f;
    public float yBound = 3.0f;
    private Transform focus = null;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        // follow the ball
        if(focus != null
            && !Input.GetKey(KeyCode.LeftShift)
            && !Input.GetKey(KeyCode.LeftAlt)
            )
        {
            float dy = focus.position.y - transform.position.y;
            if(dy < -yBound || dy > yBound)
            {
                if(transform.position.y < focus.position.y)
                {
                    delta.y = dy - yBound;
                }
                
                else
                {
                    delta.y = dy + yBound;
                }
            }
        }
        // manual control when left shift is held
        else if(Input.GetKey(KeyCode.LeftShift))
        {
            UnsetFocus();
            if(Input.GetKey(KeyCode.UpArrow))
            {
                delta = Vector3.up * manualMoveSpeed;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                delta = -Vector3.up * manualMoveSpeed;
            }

            delta *= Time.deltaTime;
        }
        // allow the player to move the camera in and out
        else if(Input.GetKey(KeyCode.LeftAlt))
        {
            UnsetFocus();
            if(Input.GetKey(KeyCode.UpArrow))
            {
                delta = Vector3.forward * manualMoveSpeed;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                delta = -Vector3.forward * manualMoveSpeed;
            }

            delta *= Time.deltaTime;
        }
        // by default, drift to the bottom of the screen
        else
        {
            delta = Vector3.down * manualMoveSpeed * Time.deltaTime;
        }

        Vector3 newPosition = transform.position + delta;
        newPosition.y = Mathf.Clamp(newPosition.y, -14.5f, 14.5f);

        transform.position = newPosition;
    }

    public void SetFocus(GameObject ball)
    {
        focus = ball.transform;
    }

    public void UnsetFocus()
    {
        focus = null;
    }
}
