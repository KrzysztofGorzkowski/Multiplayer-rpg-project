using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.Netcode;

//Stript that handles Menu UI logic in MainMenu Scene 
public class MenuUIController : NetworkBehaviour
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
        var _joinGamebutton = mainMenu.Q<Button>("joinGameButton");
        _joinGamebutton.clicked += JoinGame;

        var _startServerButton = mainMenu.Q<Button>("startServerButton");
        _startServerButton.clicked += StartServer;

        var _settingsButton = mainMenu.Q<Button>("settingsButton");
        _settingsButton.clicked += SwitchPanels;

        var _exitButton = mainMenu.Q<Button>("exitButton");
        _exitButton.clicked += Application.Quit;

        var _backButton = settings.Q<Button>("back-button");
        _backButton.clicked += SwitchPanels;
        #endregion
    }

    private void OnDisable()
    {
        var mainMenu = mainMenuPanel.rootVisualElement;
        var settings = settingsPanel.rootVisualElement;

        if (mainMenu == null || settings == null)
            return;

        #region buttonEvents
        var _joinGamebutton = mainMenu.Q<Button>("start-button");
        _joinGamebutton.clicked -= JoinGame;

        var _startServerButton = mainMenu.Q<Button>("startServerbutton");
        _startServerButton.clicked -= StartServer;

        var _settingsButton = mainMenu.Q<Button>("settings-button");
        _settingsButton.clicked -= SwitchPanels;

        var _exitButton = mainMenu.Q<Button>("exit-button");
        _exitButton.clicked -= Application.Quit;

        var _backButton = settings.Q<Button>("back-button");
        _backButton.clicked -= SwitchPanels;
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

    void StartServer()
    {
        SceneManager.LoadScene("Test");
        NetworkManager.Singleton.StartServer();
    }

    void JoinGame()
    {
        //new PlayerDatabase();
        //PlayerDatabase.ResetStats();
        //new LabyrinthDatabase();
        //LabyrinthDatabase.ResetStats();
        SceneManager.LoadScene("Test");
        //SceneManager.sceneLoaded += NetworkManager.Singleton.StartClient();
        NetworkManager.Singleton.StartClient();
    }
}