using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header ("Player and camera parameters")]
    public float playerSpeed, cameraSpeed;

    [Header ("Required GameObjects")]
    public GameObject path;
    public Camera faceCamera, sideCamera;

    
    private bool cameraShouldMove = false;

    // Movement timer
    private float timer;

    // Current path point on which the player is located
    private int currentPositionIndex = 0;

    // Which side the camera is on
    private int side;

    // Saved the initial camera position and rotation to go back to it later
    private Quaternion initialCameraRotation;
    private Vector3 initialCameraPosition;

    // Local path array and player current and next positions
    private Vector3[] pathPositions;
    private Vector3 currentPosition, nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise the camera position 
        initialCameraPosition = faceCamera.transform.localPosition;
        initialCameraRotation = faceCamera.transform.rotation;

        // Initialise the local array with the size of the path
        pathPositions = new Vector3[path.GetComponent<LineRenderer>().positionCount];

        // Fill the local array with the path positions
        path.GetComponent<LineRenderer>().GetPositions(pathPositions);

        // Get first position
        GetNextPosition();
    }

    // Function to reset the timer between points, save the current position to use in MoveTowards, and get next position if the previous one has been reached
    void GetNextPosition()
    {
        timer = 0;
        currentPosition = this.transform.position;
        if ((currentPositionIndex + 1) < pathPositions.Length)
        {
            nextPosition = pathPositions[currentPositionIndex + 1];
        }
    }
    // Update is called once per frame
    void Update()
    {
        // If game has started and the player holds the left click
        if (Input.GetMouseButton(0) && GameController.Instance.gameHasStarted == true)
        {
            MovePlayer();
        }

        // If the camera has been allowed to move (happens when the player moves trough a check point)
        if (cameraShouldMove)
        {
            MoveCamera(side);
        }

    }

    void MovePlayer()
    {
        // Increments the timer
        timer += Time.deltaTime * playerSpeed;

        // If the player hasn't yet reached the next point, MoveTowards is called
        if (this.transform.position != nextPosition)
        {
            this.transform.position = Vector3.MoveTowards(currentPosition, nextPosition, timer);
        }

        // Otherwise, the script first checks if the player has reached the finish line
        else
        {
            // If not, the next position is sent
            if (currentPositionIndex < pathPositions.Length)
            {
                GameController.Instance.progressionSlider.value += 1;
                currentPositionIndex++;
                GetNextPosition();
            }
            //If so, the victory screen is displayed 
            else
            {
                GameController.Instance.EndGame(1);
            }
        }
    }

    // Player Collide function
    private void OnTriggerEnter(Collider other)
    {
        // Depending of the tag of the collided object, the player is either killed or the camera is allowed to move
        if (other.tag == "Obstacle")
        {
            GameController.Instance.EndGame(0);
        }
        else if (other.tag == "CameraChange")
        {
            // The camera is either going one way or the other
            if (faceCamera.transform.position == sideCamera.transform.position)
            {
                side = 0;
            }
            else
            {
                side = 1;
            }

            cameraShouldMove = true;     
        }
    }

    private void MoveCamera(int side)
    {
        // Going to second position
        if (side == 1)
        {
            faceCamera.transform.position = Vector3.Lerp(faceCamera.transform.position, sideCamera.transform.position, Time.deltaTime * cameraSpeed);
            faceCamera.transform.rotation = Quaternion.Lerp(faceCamera.transform.rotation, sideCamera.transform.rotation, Time.deltaTime * cameraSpeed);
        }
        // Going back
        else
        {
            faceCamera.transform.localPosition = Vector3.Lerp(faceCamera.transform.localPosition, initialCameraPosition, Time.deltaTime * cameraSpeed);
            faceCamera.transform.rotation = Quaternion.Lerp(faceCamera.transform.rotation, initialCameraRotation, Time.deltaTime * cameraSpeed);
        }

        // Send a console to signal the camera has reached its destination after the Lerps
        if (faceCamera.transform.position == sideCamera.transform.position)
        {
            cameraShouldMove = false;
            Debug.Log("Camera has reached its destination ");
        }
        
    }

    #region Reset Functions

    // Reset the player position and next position 
    public void ResetPlayer()
    {
        currentPositionIndex = 0;
        this.transform.position = pathPositions[0];
        GetNextPosition();
    }

    //Resets the camera to the initial position and rotation
    public void ResetCamera()
    {
        cameraShouldMove = false;
        faceCamera.transform.localPosition = initialCameraPosition;
        faceCamera.transform.rotation = initialCameraRotation;
    }
    #endregion

}
