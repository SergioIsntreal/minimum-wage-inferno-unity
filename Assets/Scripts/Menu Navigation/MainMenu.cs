using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("GameLevel1");
    }

    public void GalleryButton()
    {
        SceneManager.LoadScene("Gallery");
    }

    public void BackToGallery()
    {
        SceneManager.LoadScene("Gallery");
    }

    public void ReturntoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("See you next time.");
    }
}
