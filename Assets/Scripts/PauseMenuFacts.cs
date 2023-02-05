using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuFacts : MonoBehaviour
{

    public Text factText;

    void Awake()
    {
        GenerateRandomFact();
    }

    public void GenerateRandomFact()
    {
        string[] facts = new string[] { "The English word alphabet comes (via Latin) from the names of the first two letters of the Greek alphabet: alpha and beta.",
            "The most commonly used letter from the English alphabet is E",
            "The 26 letters of the English alphabet make up more than 40 distinct sounds.",
            "The dot over the letter “i” is called title.",
            "The least commonly used letter in English is Z.",
            "About 100 languages use the same alphabet like in English which makes it one of the most widely used alphabets in the world. ",
            "The most common word in English is THE.",
            "The longest word in English which doesn’t use the letter E" };
        System.Random randomFact = new System.Random();
        int useFacts = randomFact.Next(facts.Length);
        string msgFacts = facts[useFacts];

        factText.text = msgFacts.ToString();
    }
}
