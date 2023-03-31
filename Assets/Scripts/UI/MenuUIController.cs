using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

//Stript that handles Menu UI logic in MainMenu Scene 
public class MenuUIController : MonoBehaviour
{
    //main menu panel and buttons
    public UIDocument mainMenuPanel;

    //settings panel and buttons
    public UIDocument settingsPanel;

    void OnEnable()
    {
        if (mainMenuPanel == null || settingsPanel == null)
            return;

        var mainMenu = mainMenuPanel.rootVisualElement;
        var settings = settingsPanel.rootVisualElement;

        //By default set settingsPanel's visible to false
        settings.visible = false;

        #region buttonEvents
        var startButton = mainMenu.Q<Button>("start-button");
        startButton.clicked += PlayGame;

         var settingsButton = mainMenu.Q<Button>("settings-button");
        settingsButton.clicked += SwitchPanels;

        var exitButton = mainMenu.Q<Button>("exit-button");
        exitButton.clicked += Application.Quit;

        var backButton = settings.Q<Button>("back-button");
        backButton.clicked += SwitchPanels;
        #endregion
    }

    private void OnDisable()
    {
        var mainMenu = mainMenuPanel.rootVisualElement;
        var settings = settingsPanel.rootVisualElement;

        if (mainMenu == null || settings == null)
            return;

        #region buttonEvents
        var startButton = mainMenu.Q<Button>("start-button");
        startButton.clicked -= PlayGame;

        var settingsButton = mainMenu.Q<Button>("settings-button");
        settingsButton.clicked -= SwitchPanels;

        var exitButton = mainMenu.Q<Button>("exit-button");
        exitButton.clicked -= Application.Quit;

        var backButton = settings.Q<Button>("back-button");
        backButton.clicked -= SwitchPanels;
        #endregion
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && settingsPanel.rootVisualElement.visible)
        {
            settingsPanel.rootVisualElement.visible = false;
            mainMenuPanel.rootVisualElement.visible = true;
        }
    }

    void SwitchPanels()
    {
        var mainMenu = mainMenuPanel.rootVisualElement;
        var settings = settingsPanel.rootVisualElement;
        mainMenu.visible = !mainMenu.visible;
        settings.visible = !settings.visible;
    }

    void PlayGame()
    {
        new PlayerDatabase();
        PlayerDatabase.ResetStats();
        new LabyrinthDatabase();
        LabyrinthDatabase.ResetStats();
        SceneManager.LoadScene("LabyrinthScene");
    }
}