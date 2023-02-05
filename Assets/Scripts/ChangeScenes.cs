using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{

    public Animator transition;

    public void ChooseLevel()
    {
        SceneManager.LoadScene("LevelChoose");
    }

    public void LevelOne()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void LevelThree()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void Highscore()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Highscore");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Title");
    }

    public void Information()
    {
        SceneManager.LoadScene("Information");
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);
    }
}
