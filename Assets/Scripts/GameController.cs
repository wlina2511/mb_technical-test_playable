using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{


    public static GameController Instance;
    public TextMeshProUGUI endTitle;
    public Canvas mainMenuCanvas, endCanvas;
    public GameObject player;
    public bool gameHasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToGameButton()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        gameHasStarted = true;
    }

    public void ReplayButton()
    {
        if (endCanvas.gameObject.activeSelf == true)
        {
            endCanvas.gameObject.SetActive(false);
            ResetScene();
            gameHasStarted = true;
        }
    }

    public void EndGame(int state)
    {
        endCanvas.gameObject.SetActive(true);
        if (state == 1)
        {
            endTitle.text = "VICTORY";
            endTitle.color = Color.green;
        }
        else
        {
            endTitle.text = "DEFEAT";
            endTitle.color = Color.red;
        }
        gameHasStarted = false;
    }

    public void ResetScene()
    {
        player.GetComponent<Player>().ResetPlayer();
    }
}
