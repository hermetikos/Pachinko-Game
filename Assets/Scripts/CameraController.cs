using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public float yBound = 3.0f;
    private Transform focus = null;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        // follow the ball
        if(focus != null)
        {
            Vector3 delta = Vector3.zero;

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

            transform.position += delta;
        }
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
