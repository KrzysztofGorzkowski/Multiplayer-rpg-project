using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEditor;

//Script that handles the Gamge Options Logic
public class OptionsUIController : MonoBehaviour
{
    //pause panel and buttons
    public UIDocument pausePanel;

    //settings panel and buttons
    public UIDocument settingsPanel;


    void OnEnable()
    {
        if (pausePanel == null || settingsPanel == null)
            return;

        var pause = pausePanel.rootVisualElement;
        var settings = settingsPanel.rootVisualElement;

        //by default turn off settings and pause panels
        settings.visible = false;
        pause.visible = false;

        #region buttonEvents
        var resumeButton = pause.Q<Button>("resume-button");
        resumeButton.clicked += ResumeGame;

        var settingsButton = pause.Q<Button>("settings-button");
        settingsButton.clicked += SwtichPanels;

        var menuButton = pause.Q<Button>("back-to-main-menu-button");
        menuButton.clicked += BackToMainMenu;

        var backButton = settings.Q<Button>("back-button");
        backButton.clicked += SwtichPanels;
        #endregion

    }
    void OnDisable()
    {
        var pause = pausePanel.rootVisualElement;
        var settings = settingsPanel.rootVisualElement;

        if (pause == null || settings == null)
            return;

        //by default turn off settings and pause panels
        settings.visible = false;
        pause.visible = false;

        #region buttonEvents
        var resumeButton = pause.Q<Button>("resume-button");
        resumeButton.clicked -= ResumeGame;

        var settingsButton = pause.Q<Button>("settings-button");
        settingsButton.clicked -= SwtichPanels;

        var menuButton = pause.Q<Button>("back-to-main-menu-button");
        menuButton.clicked -= BackToMainMenu;

        var backButton = settings.Q<Button>("back-button");
        backButton.clicked -= SwtichPanels;
        #endregion

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var settings = settingsPanel.rootVisualElement;
            var pause = pausePanel.rootVisualElement;
            //if settings visible turn off settings panel and go back to pause panel
            if (settings.visible)
                settings.visible = false;
            pause.visible = !pause.visible;
            //if pause panel visible freeze the game
            Time.timeScale = 1f;
            if (pause.visible)
            {
                Time.timeScale = 0f;
            }
        }
    }

    void SwtichPanels()
    {
        var settings = settingsPanel.rootVisualElement;
        var pause = pausePanel.rootVisualElement;
        pause.visible = !pause.visible;
        settings.visible = !settings.visible;
    }

    void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
        Time.timeScale = 1f;
    }

    void ResumeGame()
    {
        pausePanel.rootVisualElement.visible = false;
        Time.timeScale = 1f;
    }

}
