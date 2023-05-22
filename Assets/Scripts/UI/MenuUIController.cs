using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

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
        var _joinServer1Button = mainMenu.Q<Button>("joinServer1Button");
        _joinServer1Button.clicked += JoinServer1;

        var _joinServer2Button = mainMenu.Q<Button>("joinServer2Button");
        _joinServer2Button.clicked += JoinServer2;

        var _startServer1Button = mainMenu.Q<Button>("startServer1Button");
        _startServer1Button.clicked += StartServer1;

        var _startServer2Button = mainMenu.Q<Button>("startServer2Button");
        _startServer2Button.clicked += StartServer2;

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
        var _joinServer1Button = mainMenu.Q<Button>("joinServer1Button");
        _joinServer1Button.clicked -= JoinServer1;

        var _joinServer2Button = mainMenu.Q<Button>("joinServer2Button");
        _joinServer2Button.clicked -= JoinServer2;

        var _startServer1Button = mainMenu.Q<Button>("startServer1Button");
        _startServer1Button.clicked -= StartServer1;

        var _startServer2Button = mainMenu.Q<Button>("startServer2Button");
        _startServer2Button.clicked -= StartServer2;

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

    void StartServer1()
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("127.0.0.1", 7777, "0.0.0.0");
        //NetworkManager.Singleton.OnServerStarted +=
        NetworkManager.Singleton.StartServer();
        NetworkManager.Singleton.SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
    }

    void StartServer2()
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("127.0.0.1", 7778, "0.0.0.0");
        NetworkManager.Singleton.StartServer();
        NetworkManager.Singleton.SceneManager.LoadScene("Scene2", LoadSceneMode.Single);
    }

    public void StartingServer(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        NetworkManager.Singleton.StartServer();
    }

    void JoinServer1()
    {
        //new PlayerDatabase();
        //PlayerDatabase.ResetStats();
        //new LabyrinthDatabase();
        //LabyrinthDatabase.ResetStats();
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("127.0.0.1", 7777, "0.0.0.0");
        NetworkManager.Singleton.StartClient();
        LoadSceneServerRpc("Scene1", LoadSceneMode.Single);

    }

    void JoinServer2()
    {
        //new PlayerDatabase();
        //PlayerDatabase.ResetStats();
        //new LabyrinthDatabase();
        //LabyrinthDatabase.ResetStats();
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("127.0.0.1", 7778, "0.0.0.0");
        NetworkManager.Singleton.StartClient();
        LoadSceneServerRpc("Scene2", LoadSceneMode.Single);
        
    }
    [ServerRpc (RequireOwnership = false)]
    public void LoadSceneServerRpc(string sceneName, LoadSceneMode mode)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(sceneName, mode);
    }
}