using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameController : MonoBehaviour
{
    // References ScoreManger.cs, LetterManager.cs, and Timer.cs, respectively. 
    public ScoreManager score;
    public LetterManager letter;
    public Timer timer;
    public HighscoreManager highscore;
    public MovingSpawn car;





    public bool inputCorrect;

    // Place to show the randomly generated letter
    public char playerLetter;
    public Text letterText;

    // Variable for the randomly generated letter to be stored in
    public char startLetter;
    public char randomLetter;

    // Place to show already inputted words 
    public Text alreadyGuessed; 

    // Variables for popups 
    public GameObject popText;
    private Coroutine popTextRoutine;

    // All wordboxes
    public List<Transform> wordBoxes = new List<Transform>();
    // Dictionary
    private List<string> dictionary = new List<string>();
    // Dictionary -> List
    private List<string> guessingWords = new List<string>();
    //Already guessed words into a list
    public List<string> guessedWords;

    // Current wordbox inputting in
    private int currentWordBox;
    // Current Row, not applicable
    private int currentRow;
    // Allowed letters per row (5)
    private int charactersPerRow = 5;
    // Amount of rows (1, or 0 as written here)
    private int amountOfRows = 0;

    public AnimationCurve wordBoxInteractionCurve;





    // Function for generating a random letter
    char GetRandomLetter()
    {
        char randomLetter = (char)('a' + Random.Range(0, 26));

        Debug.Log(randomLetter);

        return randomLetter;
    }









    public void AddLetterToWordBox(string letter)
    {
        if (currentRow > amountOfRows)
        {
            Debug.Log("No more rows available");
            return;
        }
        int currentlySelectedWordbox = (currentRow * charactersPerRow) + currentWordBox;
        if (wordBoxes[currentlySelectedWordbox].GetChild(0).GetComponent<Text>().text == "")
        {
            wordBoxes[currentlySelectedWordbox].GetChild(0).GetComponent<Text>().text = letter;
            AnimateWordBox(wordBoxes[currentlySelectedWordbox]);
        }
        if (currentlySelectedWordbox < (currentRow * charactersPerRow) + 4)
        {
            currentWordBox++;
        }
    }



    public void Start()
    {
        // Finds ScoreManager.cs, LetterManager.cs, and Timer.cs respectively according to their tags.
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreManager>();
        letter = GameObject.FindGameObjectWithTag("Letter").GetComponent<LetterManager>();
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        car = GameObject.FindGameObjectWithTag("CarRace").GetComponent<MovingSpawn>();





        // Adds dictionary words to the list
        Path.Combine(Application.streamingAssetsPath, "Assets/Resources/dictionary.txt");
        AddWordsToList("Assets/Resources/dictionary.txt", dictionary);
        Path.Combine(Application.streamingAssetsPath, "Assets/Resources/wordlist.txt");
        AddWordsToList("Assets/Resources/wordlist.txt", guessingWords);

        // Generate a random letter to start with
        startLetter = GetRandomLetter();

        // Show the randomly generated letter onto the text, referencing LetterManager.cs
        playerLetter = startLetter;
        // Converts char into string so it is able to be shown on the game
        letterText.text = playerLetter.ToString();

        ShowPopText("Welcome! Your task is to write 5 words that contains a random letter. Your first letter is " + startLetter + ". The timer will start once you get your first word right.", 5f, false);


        highscore.highscoreAdd();


    }


    void AddWordsToList(string path, List<string> listOfWords)
    {
        // Scan text from file
        StreamReader reader = new StreamReader(path);
        string text = reader.ReadToEnd();

        // Text -> Console
        Debug.Log(text);

        // Seperate with ',' 
        char[] separator = { ',' };
        string[] singleWords = text.Split(separator);

        // Everyone -> List variable
        foreach (string newWord in singleWords)
        {
            listOfWords.Add(newWord);
        }

        // Close
        reader.Close();
    }



    public void RemoveLetterFromWordBox()
    {
        if (currentRow > amountOfRows)
        {
            Debug.Log("No more rows available");
            return;
        }
        int selectedWordBox = (currentRow * charactersPerRow) + currentWordBox;

        // If current box is empty, go back one and delete the one
        // that comes after
        if (wordBoxes[selectedWordBox].GetChild(0).GetComponent<Text>().text == "")
        {
            if (selectedWordBox > ((currentRow * charactersPerRow)))
            {
                // Go back
                currentWordBox--;
            }
            // Update
            selectedWordBox = (currentRow * charactersPerRow) + currentWordBox;

            wordBoxes[selectedWordBox].GetChild(0).GetComponent<Text>().text = "";
        }
        else
        {
            // If it is not empty, clear the selected. 
            wordBoxes[selectedWordBox].GetChild(0).GetComponent<Text>().text = "";
        }
        AnimateWordBox(wordBoxes[selectedWordBox]);

    }


    public void SubmitWord()
    {




        string guess = "";
        for (int i = (currentRow * charactersPerRow); i < (currentRow * charactersPerRow) + currentWordBox + 1; i++)
        {

            guess += wordBoxes[i].GetChild(0).GetComponent<Text>().text;
        }



        if (guess.Length != 5)
        {
            Debug.Log("Answer too short, must be 5 letters");
            return;
        }

        guess = guess.ToLower();

        bool wordExists = false;
        foreach (var word in guessingWords)
        {
            if (guess == word)
            {
                wordExists = true;
                break;

            }



        }

        // If inputted word (does not exist in the dictionary) 
        if (wordExists == false)
        {

            Debug.Log("No word");

            ShowPopText("Word does not exist.", 2f, false);
            return;
        }






        // If inputted word (exist in the dictionary) + (contain the letter)
        if (guess.Contains(startLetter))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            if (sceneName == "Level 2" || sceneName == "Level 3")
            {
                if (!guessedWords.Contains(guess))
                {
                    guessedWords.Add(guess);
                }
                else
                {
                    Debug.Log("Word already used.");

                    ShowPopText("You already used the word " + guess + ". Try again!", 2f, false);
                    return;
                }
                string listAsString = string.Join(", ", guessedWords.ToArray());
                alreadyGuessed.text = listAsString;
            }

           


            Debug.Log("Yes");


            int selectedWordbox = (currentRow * charactersPerRow) + currentWordBox;
            selectedWordbox = (0 * charactersPerRow) + 4;
            wordBoxes[selectedWordbox].GetChild(0).GetComponent<Text>().text = "";
            selectedWordbox = (0 * charactersPerRow) + 3;
            wordBoxes[selectedWordbox].GetChild(0).GetComponent<Text>().text = "";
            selectedWordbox = (0 * charactersPerRow) + 2;
            wordBoxes[selectedWordbox].GetChild(0).GetComponent<Text>().text = "";
            selectedWordbox = (0 * charactersPerRow) + 1;
            wordBoxes[selectedWordbox].GetChild(0).GetComponent<Text>().text = "";
            selectedWordbox = (0 * charactersPerRow) + 0;
            wordBoxes[selectedWordbox].GetChild(0).GetComponent<Text>().text = "";

            startLetter = GetRandomLetter();
            timer.RunTimer();
            car.SpeedChange();
            inputCorrect = true;



            currentWordBox = 0;

            string[] congrats = new string[] { "Superb", "Great", "Good job", "Amazing", "Incredible", "Outstanding", "Genius", "Magnificent" };
            System.Random random = new System.Random();
            int useCongrats = random.Next(congrats.Length);
            string msgCongrats = congrats[useCongrats];

            ShowPopText(msgCongrats + "! The new letter is " + startLetter + ".", 2f, false);
            playerLetter = startLetter;
            letterText.text = playerLetter.ToString();

            


            score.addScore();

            highscore.highscoreAdd();

            
        }
        else

        // If inputted word (exist in the dictionary) but (does not contain the letter)
        {
            Debug.Log("No startLetter");
            ShowPopText("Your word needs to contain the letter!", 2f, false);
            return;

        }
    }


        //        if (currentRow > amountOfRows)
        //        {
        //            Debug.Log("No more Rows");
        //            return;
        //        }


   
            
        //Popup text configuration
        void ShowPopText(string message, float duration, bool stayForever)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Level 2" || sceneName == "Level 3" || sceneName == "Level 1")
        {
            if (popTextRoutine != null)
            {
                StopCoroutine(popTextRoutine);
            }

            popTextRoutine = StartCoroutine(ShowPopupRoutine(message, duration, stayForever));
        }
      
  
        
    }
    IEnumerator ShowPopupRoutine(string message, float duration, bool stayForever = false) { 
   
        popText.transform.GetChild(0).GetComponent<Text>().text = message;
        popText.SetActive(true);
        // If it should stay forever or not
        if (stayForever)
        {
            while (true)
            {
                yield return null;
            }
        }
        // Wait for the duration time
        yield return new WaitForSeconds(duration);
        // Deactivate popText
        popText.SetActive(false);
    }

    public void timerDone()
    {
        Time.timeScale = 0;
        Debug.Log("Successfully stopped time");
    }


    IEnumerator AnimateWordboxRoutine(Transform wordboxToAnimate)
    {
        // Our timer
        float timer = 0f;

        // Duration of the animation
        float duration = 0.15f;

        //Set up startscale and end-scale of the wordbox
        Vector3 startScale = Vector3.one;

        // End-scale is just a little bit bigger than the original scale
        Vector3 scaledUp = Vector3.one * 1.2f;

        // Set the wordbox-scale to the starting scale, in case we're entering in the middle of another transition
        wordboxToAnimate.localScale = Vector3.one;

        // Loop for the time of the duration
        while (timer <= duration)
        {
            // This will go from 0 to 1 during the time of the duration
            float value = timer / duration;

            // LerpUnclamped will return a value above 1 and below 0, regular Lerp will clamp the value at 1 and 0
            // To have more freedom when animating, LerpUnclamped can be used instead
            wordboxToAnimate.localScale = Vector3.LerpUnclamped(startScale, scaledUp, wordBoxInteractionCurve.Evaluate(value));

            // Increase the timer by the delta time
            timer += Time.deltaTime;
            yield return null;
        }

        // Since we're checking if the timer is smaller and/or equals to the duration in the loop above,
        // the value might go above 1 which would give the wordbox a scale that is not equals to the desired scale.
        // To prevent slightly scaled wordboxes, we set the scale of the wordbox to the startscale
        wordboxToAnimate.localScale = startScale;
    }


    void AnimateWordBox(Transform wordboxToAnimate)
    {
        StartCoroutine(AnimateWordboxRoutine(wordboxToAnimate));
    }

}