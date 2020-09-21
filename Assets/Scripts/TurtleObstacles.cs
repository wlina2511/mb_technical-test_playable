using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleObstacles : MonoBehaviour
{
    public float backAndForthSpeed = 1.0f;
    public float initialTimer;

    Vector3 pointA;
    Vector3 pointB;
    float delay;
    float staticDelay;

    // Start is called before the first frame update
    void Start()
    {
        pointA = new Vector3(0, 0, -0.2f);
        pointB = new Vector3(0, 0, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        delay += Time.deltaTime;
        if (delay > initialTimer)
        {
            staticDelay += Time.deltaTime;
            float time = Mathf.PingPong(staticDelay * backAndForthSpeed, 1);
            transform.localPosition = Vector3.Lerp(pointA, pointB, time);
        } 
    }
}
