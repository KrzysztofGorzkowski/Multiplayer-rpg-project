using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Netcode;
public class ServerButtonsBehaviour : MonoBehaviour
{
    //main menu panel and buttons
    public UIDocument serverPanel;
    public Transform spawnObject;


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
        Transform var = Instantiate(spawnObject);
        //GameManager.LoadLabirynthServerRpc();

    }
    void HostButtonClicked()
    {
        NetworkManager.Singleton.StartHost();
        Transform var = Instantiate(spawnObject);
        //GameManager.LoadLabirynthServerRpc();
        //GameManager.LoadPlayer();
    }
    void ClientButtonClicked()
    {
        NetworkManager.Singleton.StartClient();
        Transform var = Instantiate(spawnObject);
        //GameManager.LoadLabirynth();
        //GameManager.LoadPlayer();
    }
}
