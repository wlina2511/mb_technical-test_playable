using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject path;
    public float playerSpeed, cameraSpeed;
    public Camera faceCamera, sideCamera;

    private Vector3[] pathPositions;
    private Vector3 currentPosition, nextPosition;
    private int currentPositionIndex = 0;
    private float timer;
    private bool cameraShouldMove = false;
    private int side;
    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;

    // Start is called before the first frame update
    void Start()
    {
        initialCameraPosition = faceCamera.transform.localPosition;
        initialCameraRotation = faceCamera.transform.rotation;

        Camera secondaryCamera = faceCamera;

        Debug.Log(initialCameraPosition);
        pathPositions = new Vector3[path.GetComponent<LineRenderer>().positionCount];
        path.GetComponent<LineRenderer>().GetPositions(pathPositions);
        Debug.Log(pathPositions[0]);
        GetNextPosition();
    }

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
        
        if (Input.GetMouseButton(0) && GameController.Instance.gameHasStarted == true)
        {
            Move();
        }
        if (cameraShouldMove)
        {
            MoveCamera(side);
        }

    }

    void Move()
    {
        timer += Time.deltaTime * playerSpeed;
        if (this.transform.position != nextPosition)
        {
            this.transform.position = Vector3.MoveTowards(currentPosition, nextPosition, timer);
        }
        else
        {
            if (currentPositionIndex < pathPositions.Length)
            {
                GameController.Instance.progressionSlider.value += 1;
                currentPositionIndex++;
                GetNextPosition();
            }
            else
            {
                Debug.Log("Gagné !");
                GameController.Instance.EndGame(1);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            GameController.Instance.EndGame(0);
        }
        else if (other.tag == "CameraChange")
        {

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
        
        if (side == 1)
        {
            faceCamera.transform.position = Vector3.Lerp(faceCamera.transform.position, sideCamera.transform.position, Time.deltaTime * cameraSpeed);
            faceCamera.transform.rotation = Quaternion.Lerp(faceCamera.transform.rotation, sideCamera.transform.rotation, Time.deltaTime * cameraSpeed);
            Debug.Log("Aller");
        }
        else
        {
            faceCamera.transform.localPosition = Vector3.Lerp(faceCamera.transform.localPosition, initialCameraPosition, Time.deltaTime * cameraSpeed);
            faceCamera.transform.rotation = Quaternion.Lerp(faceCamera.transform.rotation, initialCameraRotation, Time.deltaTime * cameraSpeed);
            Debug.Log("Retour");
        }

        if (faceCamera.transform.position == sideCamera.transform.position)
        {
            cameraShouldMove = false;
            Debug.Log("Camera has reached its destination ");
        }
        
    }

    public void ResetPlayer()
    {
        currentPositionIndex = 0;
        this.transform.position = pathPositions[0];
        GetNextPosition();
    }

    public void ResetCamera()
    {
        cameraShouldMove = false;
        faceCamera.transform.localPosition = initialCameraPosition;
        faceCamera.transform.rotation = initialCameraRotation;
    }

}
