using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacles : MonoBehaviour
{
    [Header("Obstacle Parameters")]
    public float rotateSpeed;

    // Update is called once per frame
    void Update()
    {        
        //Constant rotation
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);    
    }
}
