using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerUIManager : MonoBehaviour
{
    public GameObject pauseMenu;

    public buttonHover[] hoverEffects;

    public uiWipe pauseMenuWipe;

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void nextlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        pauseMenuWipe.StartWipeIn();
    }
    
    public void continueLevel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);

        foreach (buttonHover b in hoverEffects)
        {
            b.reset();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf)
        {
            pause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            continueLevel();
        }
    }
}
