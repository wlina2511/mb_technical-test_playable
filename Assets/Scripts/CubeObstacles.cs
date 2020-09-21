using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObstacles : MonoBehaviour
{
    public float delayBeforeFalling;
    public float cubeAscendingSpeed;
    //Vector3 startingPosition;

    float internalDelay = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (internalDelay > delayBeforeFalling && this.GetComponent<Rigidbody>().useGravity == false)
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            internalDelay = 0.0f;
        }
        else
        {
            internalDelay += Time.deltaTime;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, cubeAscendingSpeed, 0.0f));



    }
}
