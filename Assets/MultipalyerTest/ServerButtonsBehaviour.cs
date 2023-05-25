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
    private NetworkVariable<int> numberOfPlayers = new NetworkVariable<int>();
    public static NetworkVariable<int> numberOfPlayersOnAnotherServer = new NetworkVariable<int>();

    void OnEnable()
    {
        numberOfPlayersOnAnotherServer.Value = 0;
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
        StartCoroutine(WaitForShutdown("Scene1", LoadSceneMode.Single, 7777));
    }

    void ConnectToServer2Clicked()
    {
        NetworkManager.Singleton.Shutdown();
        StartCoroutine(WaitForShutdown("Scene2", LoadSceneMode.Single, 7778));
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Number of players on local server: " + NetworkManager.Singleton.ConnectedClients.Count.ToString() + "\n" + 
                "Number of players on another server: " + numberOfPlayersOnAnotherServer.Value.ToString());

        }

        serverPanel.rootVisualElement.Q<Label>("numberOfPlayers").text = "Connected Players: " + numberOfPlayers.Value.ToString() ;

        if (!IsServer)
        {
            return;
        }
        numberOfPlayers.Value = NetworkManager.Singleton.ConnectedClients.Count + numberOfPlayersOnAnotherServer.Value;
    }

    [ServerRpc(RequireOwnership = false)]
    public void LoadSceneServerRpc(string sceneName, LoadSceneMode mode)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(sceneName, mode);
    }

    IEnumerator WaitForShutdown(string sceneName, LoadSceneMode mode, ushort portNumber)
    {
        yield return new WaitForSeconds(1f);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("127.0.0.1", portNumber, "0.0.0.0");
        NetworkManager.Singleton.StartClient();
        LoadSceneServerRpc(sceneName, mode);
    }
}
