using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject path;
    public float moveSpeed;
    public Camera faceCamera, sideCamera;

    private Vector3[] pathPositions;
    private Vector3 currentPosition, nextPosition;
    private int currentPositionIndex;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        pathPositions = new Vector3[path.GetComponent<LineRenderer>().positionCount];
        path.GetComponent<LineRenderer>().GetPositions(pathPositions);
        Debug.Log(pathPositions[0]);
        GetNextPosition();
        //this.transform.position = pathPositions[0];
    }

    void GetNextPosition()
    {
        timer = 0;
        currentPosition = this.transform.position;
        nextPosition = pathPositions[currentPositionIndex + 1];
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            Move();
        }
        
    }

    void Move()
    {
        timer += Time.deltaTime * moveSpeed;
        if (this.transform.position != nextPosition)
        {
            //this.transform.position = Vector3.Lerp(currentPosition, nextPosition, timer);
            this.transform.position = Vector3.MoveTowards(currentPosition, nextPosition, timer);
        }
        else
        {
            if (currentPositionIndex < pathPositions.Length)
            {
                currentPositionIndex++;
                GetNextPosition();
            }
            else
            {
                Debug.Log("Gagné !");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            Debug.Log("perdu");
        }
        else if (other.tag == "CameraChange")
        {
            if(faceCamera.gameObject.activeSelf == true)
            {
                faceCamera.gameObject.SetActive(false);
                sideCamera.gameObject.SetActive(true);
            }
            else
            {
                sideCamera.gameObject.SetActive(false);
                faceCamera.gameObject.SetActive(true);
            }
        }

    }
}
