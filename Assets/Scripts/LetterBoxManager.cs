using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBoxManager : MonoBehaviour
{
    public GameObject wordBoxes;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(wordBoxes, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
