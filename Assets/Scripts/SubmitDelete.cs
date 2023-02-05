using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitDelete : MonoBehaviour
{

    public GameController game;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("Game").GetComponent<GameController>();
    }

    // Update is called once per frame
    public void ClickEnter()
    {
        game.SubmitWord();
    }

    public void ClickDelete()
    {
        game.RemoveLetterFromWordBox();
    }
}
