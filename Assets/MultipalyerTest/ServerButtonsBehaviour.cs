using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Netcode;
public class ServerButtonsBehaviour : NetworkBehaviour
{
    //main menu panel and buttons
    public UIDocument serverPanel;
    public Transform spawnObject;

    private NetworkVariable<int> numberOfPlayers = new NetworkVariable<int>();


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

        var connectToServer1Button = _serverPanel.Q<Button>("connectToServer1");
        serverButton.clicked += ConnectToServer1Clicked;

        var connectToServer2Button = _serverPanel.Q<Button>("connectToServer2");
        serverButton.clicked += ConnectToServer2Clicked;
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

    void ConnectToServer1Clicked()
    {

    }


    void ConnectToServer2Clicked()
    {

    }



    private void Update()
    {
        serverPanel.rootVisualElement.Q<Label>("numberOfPlayers").text = numberOfPlayers.Value.ToString();

        if (!IsServer)
        {
            return;
        }

        numberOfPlayers.Value = NetworkManager.Singleton.ConnectedClients.Count;
    }
}
