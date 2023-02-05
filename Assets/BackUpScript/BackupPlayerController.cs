
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//public class PlayerController : MonoBehaviour
//{

//    public List<Button> keyboardCharacterButtons = new List<Button>();


//    private string characterNames = "QWERTYUIOPASDFGHJKLZXCVBNM";


//    public GameController gameController;


//    void Start()
//    {
//        SetupButtons(GetKeyboardCharacterButtons());
//    }

//    private List<Button> GetKeyboardCharacterButtons()
//    {
//        return keyboardCharacterButtons;
//    }

//    void SetupButtons(List<Button> keyboardCharacterButtons)
//    {
     
//        for (int i = 0; i < keyboardCharacterButtons.Count; i++)
//        {
//            keyboardCharacterButtons[i].transform.GetChild(0)
//                .GetComponent<Text>().text = characterNames[i].ToString();
//        }
      
//        foreach (var keyboardButton in keyboardCharacterButtons)
//        {
//            string letter = keyboardButton.transform.GetChild(0).GetComponent<Text>().text;
//            keyboardButton.GetComponent<Button>().onClick.AddListener(() => ClickCharacter(letter));
//        }
//    }
//    void ClickCharacter(string letter)
//    {

//        Debug.Log(letter);
//        gameController.AddLetterToWordBox(letter);
//    }
 
//    void Update()
//    {
//    }
//}