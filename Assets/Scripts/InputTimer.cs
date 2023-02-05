using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class InputTimer : MonoBehaviour
{
    public float inputTimeRemaining = 10;
    public bool inputTimerIsRunning = false;
    public Text inputTimeText;
    public GameController game;
    public GameObject timeOvermenu;

    void Start()
    {

        game = GameObject.FindGameObjectWithTag("Game").GetComponent<GameController>();
        timeOvermenu.SetActive(false);
    }


    void Update()
    {

        if (game.inputCorrect == true)
        {
            Debug.Log("Input Timer started");
            // Starts the timer once RunTimer is enabled
            inputTimerIsRunning = true;
            inputTimeRemaining = 5;

            game.inputCorrect = false;
        }

        if (inputTimerIsRunning)
        {
            if (inputTimeRemaining > 0)
            {
                inputTimeRemaining -= Time.deltaTime;
                DisplayTime(inputTimeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeOvermenu.SetActive(true);
                inputTimeRemaining = 0;
                inputTimerIsRunning = false;

            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        inputTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}