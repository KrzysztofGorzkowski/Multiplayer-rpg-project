using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Netcode;
public class ServerButtonsBehaviour : MonoBehaviour
{
    //main menu panel and buttons
    public UIDocument serverPanel;


    void OnEnable()
    {
        if (serverPanel == null)
            return;

        var _serverPanel = serverPanel.rootVisualElement;

        #region buttonEvents
        var serverButton = _serverPanel.Q<Button>("serverButton");
        serverButton.clicked += ServerButtonClicked;

        var hostButton = _serverPanel.Q<Button>("hostButton");
        hostButton.clicked += HostButtonClicked;

        var clientButton = _serverPanel.Q<Button>("clientButton");
        clientButton.clicked += ClientButtonClicked;
        #endregion
    }

    void ServerButtonClicked()
    {
        NetworkManager.Singleton.StartServer();
        GameManager.LoadLabirynth();

    }
    void HostButtonClicked()
    {
        NetworkManager.Singleton.StartHost();
        GameManager.LoadLabirynth();
        //GameManager.LoadPlayer();
    }
    void ClientButtonClicked()
    {
        NetworkManager.Singleton.StartClient();
        //GameManager.LoadLabirynth();
        //GameManager.LoadPlayer();
    }
}