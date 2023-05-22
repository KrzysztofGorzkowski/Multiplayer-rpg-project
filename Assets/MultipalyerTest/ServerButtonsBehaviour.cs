using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;
using Unity.Netcode.Transports.UTP;
using Unity.Networking;
using UnityEngine.SceneManagement;

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
        var connectToServer1Button = _serverPanel.Q<Button>("connectToServer1");
        connectToServer1Button.clicked += ConnectToServer1Clicked;

        var connectToServer2Button = _serverPanel.Q<Button>("connectToServer2");
        connectToServer2Button.clicked += ConnectToServer2Clicked;
        #endregion
    }



    void ConnectToServer1Clicked()
    {
        NetworkManager.Singleton.Shutdown();
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("127.0.0.1", 7777, "0.0.0.0");
        NetworkManager.Singleton.StartClient();
        LoadSceneServerRpc("Scene1", LoadSceneMode.Single);
        //Transform var = Instantiate(spawnObject);
    }


    void ConnectToServer2Clicked()
    {
        NetworkManager.Singleton.Shutdown();
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("127.0.0.1", 7778, "0.0.0.0");
        NetworkManager.Singleton.StartClient();
        LoadSceneServerRpc("Scene2", LoadSceneMode.Single);
        //Transform var = Instantiate(spawnObject);
    }

    private void Update()
    {
        serverPanel.rootVisualElement.Q<Label>("numberOfPlayers").text = "Connected Players: " + numberOfPlayers.Value.ToString();

        if (!IsServer)
        {
            return;
        }

        numberOfPlayers.Value = NetworkManager.Singleton.ConnectedClients.Count;
    }

    [ServerRpc(RequireOwnership = false)]
    public void LoadSceneServerRpc(string sceneName, LoadSceneMode mode)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(sceneName, mode);
    }
}
