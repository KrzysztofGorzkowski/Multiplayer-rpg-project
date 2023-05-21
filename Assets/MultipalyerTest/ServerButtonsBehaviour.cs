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
        //NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("127.0.0.1", 7777, "0.0.0.0");
        //NetworkManager.Singleton.StartClient();
        SceneManager.LoadScene("Scene1");
        //SceneManager.sceneLoaded += JoiningServer;
        //Transform var = Instantiate(spawnObject);
    }


    void ConnectToServer2Clicked()
    {
        //NetworkManager.Singleton.SceneManager.
        NetworkManager.Singleton.Shutdown();
        //NetworkManager.Singleton.OnClientDisconnectCallback += Method;
        //NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("127.0.0.1", 7778, "0.0.0.0");
        //NetworkManager.Singleton.StartClient();
        SceneManager.LoadScene("Scene2");
        //SceneManager.sceneLoaded += JoiningServer;
        //Transform var = Instantiate(spawnObject);
    }

    public void Method(ulong temp)
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("127.0.0.1", 7778, "0.0.0.0");
        NetworkManager.Singleton.StartClient();
    }

    public void JoiningServer(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        NetworkManager.Singleton.StartClient();
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
}
