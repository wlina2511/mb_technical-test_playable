﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{


    public static GameController Instance;
    public TextMeshProUGUI endTitle, gameTimerText, endTimerText;
    public Canvas mainMenuCanvas, endCanvas;
    public GameObject player;

    public GameObject endTimer;
    public bool gameHasStarted = false;


    private float gameTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHasStarted)
        {
            gameTimer += Time.deltaTime;
            gameTimerText.text = gameTimer.ToString("F1");
        }
        
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
            endTimer.gameObject.SetActive(true);
            endTimerText.text = gameTimerText.text;
            
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
        gameTimer = 0;
        player.GetComponent<Player>().ResetPlayer();
        player.GetComponent<Player>().ResetCamera();
    }
}
