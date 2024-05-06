using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Main;
    public GameObject Credits;

    void Start()
    {
        Main.SetActive(true);
        Credits.SetActive(false);
    }

    public void GoCredits()
    {
        Credits.SetActive(true);
        Main.SetActive(false);
    }

    public void GoMain()
    {
        Main.SetActive(true);
        Credits.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void StartMain()
    {
        SceneManager.LoadScene(0);
    }
}
