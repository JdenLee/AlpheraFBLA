
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{

    public List<Button> keyboardCharacterButtons = new List<Button>();

    public KeyCode _key;
    private Button _button;


    private string characterNames = "QWERTYUIOPASDFGHJKLZXCVBNM";


    public GameController gameController;


    void Awake()
    {
        _button = GetComponent<Button>();

    }

    public void Start()
    {
        SetupButtons(GetKeyboardCharacterButtons());
    }

    private List<Button> GetKeyboardCharacterButtons()
    {
        return keyboardCharacterButtons;
    }

    void Update()
    {
        if (Input.GetKeyDown(_key))
        {
            FadeToColor(_button.colors.pressedColor);
            // Click the button
            _button.onClick.Invoke();
        }
        else if (Input.GetKeyUp(_key))
        {
            FadeToColor(_button.colors.normalColor);
        }


    }

    void FadeToColor(Color color)
    {
        Graphic graphic = GetComponent<Graphic>();
        graphic.CrossFadeColor(color, _button.colors.fadeDuration, true, true);
    }

    void SetupButtons(List<Button> keyboardCharacterButtons)
    {
     
        for (int i = 0; i < keyboardCharacterButtons.Count; i++)
        {
            keyboardCharacterButtons[i].transform.GetChild(0)
                .GetComponent<Text>().text = characterNames[i].ToString();
        }
      
        foreach (var keyboardButton in keyboardCharacterButtons)
        {
            string letter = keyboardButton.transform.GetChild(0).GetComponent<Text>().text;
            keyboardButton.GetComponent<Button>().onClick.AddListener(() => ClickCharacter(letter));
        }
    }
    void ClickCharacter(string letter)
    {

        Debug.Log(letter);
        gameController.AddLetterToWordBox(letter);
    }
 

}