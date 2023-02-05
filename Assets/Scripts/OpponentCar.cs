using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentCar : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 startPosition = new Vector2(-5, 0);
    public Vector2 endPosition = new Vector2(5, 0);


    void Start()
    {
        transform.position = startPosition;
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
        }
    }
}