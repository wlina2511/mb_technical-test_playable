using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("Game Options, shouldn't be touched")]
    // Freeze the game before playing and after loosing
    public bool gameHasStarted = false;

    [Header("Required GameObjects")]
    public Canvas mainMenuCanvas, gameCanvas, endCanvas;
    public TextMeshProUGUI endTitle, gameTimerText, endTimerText;
    public GameObject player, path;
    public GameObject endTimer;
    public Slider progressionSlider;

    public static GameController Instance;

    // Internal timer
    private float gameTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Setting the singleton and setting up the slider's min and max values
        Instance = this;
        progressionSlider.minValue = 0;
        progressionSlider.maxValue = path.GetComponent<LineRenderer>().positionCount;
    }

    // Update is called once per frame
    void Update()
    {
        // Increments the timer if game has started, rounded to the first decimal
        if (gameHasStarted)
        {
            gameTimer += Time.deltaTime;
            gameTimerText.text = gameTimer.ToString("F1");
        }
        
    }

 
    #region Button Functions

    // Launch game after pressing the "Play" button
    public void ToGameButton()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(true);
        gameHasStarted = true;
    }

    // Replay game and resets the scene
    public void ReplayButton()
    {
        if (endCanvas.gameObject.activeSelf == true)
        {
            endCanvas.gameObject.SetActive(false);
            ResetScene();
            gameHasStarted = true;
        }
    }
    #endregion

    // Function that is called at the end of the game, whether the player has won or not; state == 1 : won, state  == 0 : lost
    public void EndGame(int state)
    {
        // If the player won, display the victory screen and its score
        endCanvas.gameObject.SetActive(true);
        if (state == 1)
        {
            endTitle.text = "VICTORY";
            endTitle.color = Color.green;
            endTimer.gameObject.SetActive(true);
            endTimerText.text = gameTimerText.text;
            
        }
        // If he lost, only the replay button is displayed
        else
        {
            endTitle.text = "DEFEAT";
            endTitle.color = Color.red;
        }
        gameHasStarted = false;
    }

    // Combine the different reset functions to completely reset the scene, including score, slider, player and camera.
    public void ResetScene()
    {
        gameTimer = 0;
        progressionSlider.value = progressionSlider.minValue;
        player.GetComponent<Player>().ResetPlayer();
        player.GetComponent<Player>().ResetCamera();
    }
}
