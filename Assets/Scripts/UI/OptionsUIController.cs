using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEditor;
using Unity.Netcode;

//Script that handles the Gamge Options Logic
public class OptionsUIController : NetworkBehaviour
{
    //options panel and buttons
    public UIDocument optionsPanel;

    //settings panel and buttons
    public UIDocument settingsPanel;
    public UIDocument serverPanel;


    void OnEnable()
    {
        if (optionsPanel == null || settingsPanel == null) return;

        var options = optionsPanel.rootVisualElement;
        var settings = settingsPanel.rootVisualElement;

        //by default turn off settings and options panels
        settings.visible = false;
        options.visible = false;

        #region buttonEvents
        var resumeButton = options.Q<Button>("resume-button");
        resumeButton.clicked += ExitOptions;

        var settingsButton = options.Q<Button>("settings-button");
        settingsButton.clicked += SwtichPanels;

        var menuButton = options.Q<Button>("back-to-main-menu-button");
        menuButton.clicked += BackToMainMenu;

        var backButton = settings.Q<Button>("back-button");
        backButton.clicked += SwtichPanels;
        #endregion

    }
    void OnDisable()
    {
        var options = optionsPanel.rootVisualElement;
        var settings = settingsPanel.rootVisualElement;

        if (options == null || settings == null) return;

        //by default turn off settings and options panels
        settings.visible = false;
        options.visible = false;

        #region buttonEvents
        var resumeButton = options.Q<Button>("resume-button");
        resumeButton.clicked -= ExitOptions;

        var settingsButton = options.Q<Button>("settings-button");
        settingsButton.clicked -= SwtichPanels;

        var menuButton = options.Q<Button>("back-to-main-menu-button");
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
            var options = optionsPanel.rootVisualElement;
            var _serverPanel = serverPanel.rootVisualElement;
            //if settings visible turn off settings panel and go back to options panel
            if (settings.visible)
                settings.visible = false;
            options.visible = !options.visible;
            _serverPanel.visible = !options.visible;
            //Debug.Log(options.visible.ToString());
        }
    }

    void SwtichPanels()
    {
        var settings = settingsPanel.rootVisualElement;
        var options = optionsPanel.rootVisualElement;
        options.visible = !options.visible;
        settings.visible = !settings.visible;
    }

    void BackToMainMenu()
    {
        NetworkManager.Singleton.Shutdown();
        NetworkManager.Singleton.SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }

    void ExitOptions()
    {
        optionsPanel.rootVisualElement.visible = false;
    }

}
