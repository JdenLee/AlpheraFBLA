using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public Text gameEndScore;



    [ContextMenu("Increase Score")]
    public void addScore()
    {
        playerScore = playerScore + 100;
        scoreText.text = playerScore.ToString();
        gameEndScore.text = playerScore.ToString();

    }


}
