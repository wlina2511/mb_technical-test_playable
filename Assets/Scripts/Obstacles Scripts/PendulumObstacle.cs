using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumObstacle : MonoBehaviour
{

    [Header("Obstacle Parameters")]
    public float pendulumAngle = 90.0f;
    public float pendulumSpeed = 2.0f;
    public float initialTimer =0.0f;


    Quaternion start, end;
    // Start is called before the first frame update
    void Start()
    {
        // Define the max angles, negative and positive, of the pendulum
        start = PendulumRotation(pendulumAngle);
        end = PendulumRotation(-pendulumAngle);
    }

    // Using FixedUpdate to ensure no alteration from the game performances
    // Not used in other obstacles because their movement are much simpler
    private void FixedUpdate()
    {
        initialTimer += Time.deltaTime;

        // Rotation around the pivot point
        transform.rotation = Quaternion.Lerp(start, end, (Mathf.Sin(initialTimer * pendulumSpeed + Mathf.PI / 2) + 1.0f) / 2.0f);
    }

    // Function to convert an angle into a Quaternion
    Quaternion PendulumRotation(float angle)
    {
        Quaternion pendulumRotation = transform.rotation;
        float angleZ = pendulumRotation.eulerAngles.z + angle;

        if (angleZ > 180)
        {
            angleZ -= 360;
        }else if( angleZ < -180)
        {
            angleZ += 360;
        }

        pendulumRotation.eulerAngles = new Vector3(pendulumRotation.eulerAngles.x, pendulumRotation.eulerAngles.y, angleZ);

        return pendulumRotation;
    }
}
