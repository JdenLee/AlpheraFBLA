using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingSpawn : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 startPosition = new Vector2(-5, 0);
    public Vector2 endPosition = new Vector2(5, 0);

    public Text speedText;

    public GameController game;
    public ScoreManager score;


    void Start()
    {
        transform.position = startPosition;
        game = GameObject.FindGameObjectWithTag("Game").GetComponent<GameController>();
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreManager>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish")
        {
            transform.position = startPosition;
            score.playerScore += 100;
            score.scoreText.text = score.playerScore.ToString();
            score.gameEndScore.text = score.playerScore.ToString();
            speed -= 20f;
            speedText.text = speed.ToString();

        }
    }

    public void SpeedChange()
    {
        speed += 5f;
        speedText.text = speed.ToString();
    }
}