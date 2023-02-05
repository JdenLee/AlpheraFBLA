using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour
{
    public ScoreManager score;
    public Text lvl1score, lvl2score, lvl3score;

    void start()
    {
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreManager>();
        
    }


    void Awake()
    {
        updateScoreboard();

    }

    void Update()
    {
        updateScoreboard();

    }

    void updateScoreboard()
    {
        lvl1score.text = PlayerPrefs.GetFloat("lvl1Score", 0).ToString("0");
        lvl2score.text = PlayerPrefs.GetFloat("lvl2Score", 0).ToString("0");
        lvl3score.text = PlayerPrefs.GetFloat("lvl3Score", 0).ToString("0");
    }

    public void highscoreAdd()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // Save highscores to PlayerPref
        if (sceneName == "Level 1" && score.playerScore > PlayerPrefs.GetFloat("lvl1Score"))
        {
            PlayerPrefs.SetFloat("lvl1Score", score.playerScore);
        }
        else if (sceneName == "Level 2" && score.playerScore > PlayerPrefs.GetFloat("lvl2Score"))
        {
            PlayerPrefs.SetFloat("lvl2Score", score.playerScore);
        }
        else if (sceneName == "Level 3" && score.playerScore > PlayerPrefs.GetFloat("lvl3Score"))
        {
            PlayerPrefs.SetFloat("lvl3Score", score.playerScore);
        }
    }

    public void resetScoreboard()
    {
        PlayerPrefs.SetFloat("lvl1Score", 0);
        PlayerPrefs.SetFloat("lvl2Score", 0);
        PlayerPrefs.SetFloat("lvl3Score", 0);
        updateScoreboard();
    }
}