//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;
//using UnityEngine.UI;



//public class GameController : MonoBehaviour
//{
//    public ScoreManager score;
//    public LetterManager letter;
//    public Timer timer;

//    public char playerLetter;
//    public Text letterText;

//    public char startLetter;

//    public char randomLetter;

//    public Text alreadyGuessed;

//    public GameObject popText;
//    private Coroutine popTextRoutine;

//    // All wordboxes
//    public List<Transform> wordBoxes = new List<Transform>();

//    private List<string> dictionary = new List<string>();

//    private List<string> guessingWords = new List<string>();

//    public List<string> guessedWords;

//    // Current wordbox inputting in
//    private int currentWordBox;


//    private int currentRow;


//    private int charactersPerRow = 5;

//    private int amountOfRows = 0;

//    public KeyCode _key;
//    private Button _button;




//    void Awake()
//    {
//        _button = GetComponent<Button>();
//    }

//    char GetRandomLetter()
//    {
//        char randomLetter = (char)('a' + Random.Range(0, 26));

//        Debug.Log(randomLetter);

//        return randomLetter;
//    }


//    void Update()
//    {
//        if (Input.GetKeyDown(_key))
//        {
//            FadeToColor(_button.colors.pressedColor);
//            // Click the button
//            _button.onClick.Invoke();
//        }
//        else if (Input.GetKeyUp(_key))
//        {
//            FadeToColor(_button.colors.normalColor);
//        }
//    }

//    void FadeToColor(Color color)
//    {
//        Graphic graphic = GetComponent<Graphic>();
//        graphic.CrossFadeColor(color, _button.colors.fadeDuration, true, true);
//    }





//    public void AddLetterToWordBox(string letter)
//    {
//        if (currentRow > amountOfRows)
//        {
//            Debug.Log("No more rows available");
//            return;
//        }
//        int currentlySelectedWordbox = (currentRow * charactersPerRow) + currentWordBox;
//        if (wordBoxes[currentlySelectedWordbox].GetChild(0).GetComponent<Text>().text == "")
//        {
//            wordBoxes[currentlySelectedWordbox].GetChild(0).GetComponent<Text>().text = letter;
//        }
//        if (currentlySelectedWordbox < (currentRow * charactersPerRow) + 4)
//        {
//            currentWordBox++;
//        }
//    }



//    void Start()
//    {
//        score = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreManager>();
//        letter = GameObject.FindGameObjectWithTag("Letter").GetComponent<LetterManager>();
//        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
//        AddWordsToList("Assets/Resources/dictionary.txt", dictionary);


//        AddWordsToList("Assets/Resources/wordlist.txt", guessingWords);

//        startLetter = GetRandomLetter();

//        playerLetter = startLetter;
//        letterText.text = playerLetter.ToString();

//        ShowPopText("Welcome! Your task is to write 5 words that contains a random letter. Your first letter is " + startLetter + ". The timer will start once you get your first word right.", 5f, false);


//    }


//    void AddWordsToList(string path, List<string> listOfWords)
//    {
//        // Scan text from file
//        StreamReader reader = new StreamReader(path);
//        string text = reader.ReadToEnd();

//        // Text -> Console
//        Debug.Log(text);

//        // Seperate with ',' 
//        char[] separator = { ',' };
//        string[] singleWords = text.Split(separator);

//        // Everyone -> List variable
//        foreach (string newWord in singleWords)
//        {
//            listOfWords.Add(newWord);
//        }

//        // Close
//        reader.Close();
//    }



//    public void RemoveLetterFromWordBox()
//    {
//        if (currentRow > amountOfRows)
//        {
//            Debug.Log("No more rows available");
//            return;
//        }
//        int selectedWordBox = (currentRow * charactersPerRow) + currentWordBox;

//        // If current box is empty, go back one and delete the one
//        // that comes after
//        if (wordBoxes[selectedWordBox].GetChild(0).GetComponent<Text>().text == "")
//        {
//            if (selectedWordBox > ((currentRow * charactersPerRow)))
//            {
//                // Go back
//                currentWordBox--;
//            }
//            // Update
//            selectedWordBox = (currentRow * charactersPerRow) + currentWordBox;

//            wordBoxes[selectedWordBox].GetChild(0).GetComponent<Text>().text = "";
//        }
//        else
//        {
//            // If it is not empty, clear the selected. 
//            wordBoxes[selectedWordBox].GetChild(0).GetComponent<Text>().text = "";
//        }

//    }


//    public void SubmitWord()
//    {




//        string guess = "";
//        for (int i = (currentRow * charactersPerRow); i < (currentRow * charactersPerRow) + currentWordBox + 1; i++)
//        {

//            guess += wordBoxes[i].GetChild(0).GetComponent<Text>().text;
//        }



//        if (guess.Length != 5)
//        {
//            Debug.Log("Answer too short, must be 5 letters");
//            return;
//        }

//        guess = guess.ToLower();

//        bool wordExists = false;
//        foreach (var word in guessingWords)
//        {
//            if (guess == word)
//            {
//                wordExists = true;
//                break;

//            }



//        }

//        // If inputted word (does not exist in the dictionary) 
//        if (wordExists == false)
//        {

//            Debug.Log("No word");

//            ShowPopText("Word does not exist.", 2f, false);
//            return;
//        }






//        // If inputted word (exist in the dictionary) + (contain the letter)
//        if (guess.Contains(startLetter))
//        {


//            if (!guessedWords.Contains(guess))
//            {
//                guessedWords.Add(guess);
//            }
//            else
//            {
//                Debug.Log("Word alreayd used.");

//                ShowPopText("You already used the word " + guess + ". Try again!", 2f, false);
//                return;
//            }
//            string listAsString = string.Join(", ", guessedWords.ToArray());
//            alreadyGuessed.text = listAsString;


//            Debug.Log("Yes");


//            int selectedWordbox = (currentRow * charactersPerRow) + currentWordBox;
//            selectedWordbox = (0 * charactersPerRow) + 4;
//            wordBoxes[selectedWordbox].GetChild(0).GetComponent<Text>().text = "";
//            selectedWordbox = (0 * charactersPerRow) + 3;
//            wordBoxes[selectedWordbox].GetChild(0).GetComponent<Text>().text = "";
//            selectedWordbox = (0 * charactersPerRow) + 2;
//            wordBoxes[selectedWordbox].GetChild(0).GetComponent<Text>().text = "";
//            selectedWordbox = (0 * charactersPerRow) + 1;
//            wordBoxes[selectedWordbox].GetChild(0).GetComponent<Text>().text = "";
//            selectedWordbox = (0 * charactersPerRow) + 0;
//            wordBoxes[selectedWordbox].GetChild(0).GetComponent<Text>().text = "";

//            startLetter = GetRandomLetter();
//            timer.RunTimer();

//            currentWordBox = 0;

//            string[] congrats = new string[] { "Superb", "Great", "Good job", "Amazing", "Incredible", "Outstanding", "Genius", "Magnificent" };
//            System.Random random = new System.Random();
//            int useCongrats = random.Next(congrats.Length);
//            string msgCongrats = congrats[useCongrats];

//            ShowPopText(msgCongrats + "! The new letter is " + startLetter + ".", 2f, false);
//            playerLetter = startLetter;
//            letterText.text = playerLetter.ToString();




//            score.addScore();
//        }
//        else

//        // If inputted word (exist in the dictionary) but (does not contain the letter)
//        {
//            Debug.Log("No startLetter");
//            ShowPopText("Your word needs to contain the letter!", 2f, false);
//            return;

//        }

//        if (currentRow > amountOfRows)
//        {
//            Debug.Log("No more Rows");
//            return;
//        }



//        //if (point == 5)
//        //{
//        //    Debug.Log("Game won");
//        //    ShowPopup("Congratulations! You have beaten the game!", 0f, true);
//        //    return;
//        //}



//    }



//    void ShowPopText(string message, float duration, bool stayForever)
//    {

//        if (popTextRoutine != null)
//        {
//            StopCoroutine(popTextRoutine);
//        }
//        popTextRoutine = StartCoroutine(ShowPopupRoutine(message, duration, stayForever));
//    }
//    IEnumerator ShowPopupRoutine(string message, float duration, bool stayForever = false)
//    {

//        popText.transform.GetChild(0).GetComponent<Text>().text = message;
//        popText.SetActive(true);
//        // If it should stay forever or not
//        if (stayForever)
//        {
//            while (true)
//            {
//                yield return null;
//            }
//        }
//        // Wait for the duration time
//        yield return new WaitForSeconds(duration);
//        // Deactivate popText
//        popText.SetActive(false);
//    }
//}