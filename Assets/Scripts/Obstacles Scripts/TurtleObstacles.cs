using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleObstacles : MonoBehaviour
{
    [Header ("Obstacle Parameters")]
    public float backAndForthSpeed = 1.0f;

    // After how much time the obstacles will start moving
    // Used to desynchronise instances of the same obstacle
    public float initialTimer;

    // Back and forth points
    Vector3 pointA;
    Vector3 pointB;

    float internalDelay = 0;    
    float pingPongTime;

    // Start is called before the first frame update
    void Start()
    {
        pointA = new Vector3(0, 0, -0.2f);
        pointB = new Vector3(0, 0, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        // Wait before starting to move
        if (internalDelay > initialTimer)
        {
            pingPongTime += Time.deltaTime;
            float time = Mathf.PingPong(pingPongTime * backAndForthSpeed, 1);
            transform.localPosition = Vector3.Lerp(pointA, pointB, time);
        }
        else
        {
            internalDelay += Time.deltaTime;
        }
    }
}
