using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverUIController : MonoBehaviour
{
    //main menu panel and buttons
    public UIDocument gameOverPanel;

    void OnEnable()
    {
        var root = gameOverPanel.rootVisualElement;

        #region buttonEvents
        var playAgainButton = root.Q<Button>("play-again-button");
        playAgainButton.clicked += PlayAgain;

        var exitButton = root.Q<Button>("exit-button");
        exitButton.clicked += Application.Quit;

        var backButton = root.Q<Button>("back-to-menu-button");
        backButton.clicked += LoadMainMenuScene;
        #endregion
    }

    private void OnDisable()
    {
        var root = gameOverPanel.rootVisualElement;

        if (root == null)
            return;

        #region buttonEvents
        var playAgainButton = root.Q<Button>("play-again-button");
        playAgainButton.clicked -= PlayAgain;

        var exitButton = root.Q<Button>("exit-button");
        exitButton.clicked -= Application.Quit;

        var backButton = root.Q<Button>("back-to-menu-button");
        backButton.clicked -= LoadMainMenuScene;
        #endregion
    }

    void PlayAgain()
    {
        PlayerDatabase.ResetStats();
        LabyrinthDatabase.ResetStats();
        SceneManager.LoadScene("LabyrinthScene");
    }

    void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
