using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnScript : MonoBehaviour
{
    public float respawnTime = 5f;
    public Vector2 spawnAreaMin = new Vector2(-5, -5);
    public Vector2 spawnAreaMax = new Vector2(5, 5);
    public Text randomText;
    public GameController game;

    private void Start()
    {
        InvokeRepeating("Respawn", respawnTime, respawnTime);
        game = GameObject.FindGameObjectWithTag("Game").GetComponent<GameController>();
    }

    private void Respawn()
    {
        string[] congrats = new string[] { "Brown", "Times", "Mouse", "Money", "Paper", "Crown", "Annex", "Quail", "Hacks", "Nouns", "House", "Wised", "Viced", "Power", "Zebra", "Quais", "Highs", "Hijab", "Books", "Lamps", "Cooks", "Light" };
        System.Random random = new System.Random();
        int useCongrats = random.Next(congrats.Length);
        string msgCongrats = congrats[useCongrats];

        randomText.text = msgCongrats.ToString();

        transform.position = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)

        );
    }
}