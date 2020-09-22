using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObstacles : MonoBehaviour
{
    [Header("Obstacle Parameters")]
    // Those two variables are strongly tied
    public float delayBeforeFalling;
    public float cubeAscendingSpeed;


    float internalDelay = 0;

    // Update is called once per frame
    void Update()
    {
        // If the delay is over, drop the cube using gravity
        if (internalDelay > delayBeforeFalling && GetComponent<Rigidbody>().useGravity == false)
        {
            GetComponent<Rigidbody>().useGravity = true;
            internalDelay = 0.0f;
        }
        else
        {
            internalDelay += Time.deltaTime;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        // When the cube touches the ground after dropping, a force is applied to the rigidBody and gravity is disabled 
        GetComponent<Rigidbody>().useGravity = false;

        // Making sure the speed of the rigidBody is null when launching up, in case some exterior forces are applied (it shouldn't happen but we're never sure)
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, cubeAscendingSpeed, 0.0f));
    }
}
