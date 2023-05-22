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
    

    public override void OnNetworkSpawn()
    {
        
    }

    void ConnectToServer1Clicked()
    {
        NetworkManager.Singleton.Shutdown();
        //StartCoroutine(WaitForShutdown());
        Debug.Log("Before Setting a Connection");
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("127.0.0.1", 7777, "0.0.0.0");
        Debug.Log("After Setting a Connection");
        NetworkManager.Singleton.StartClient();
        Debug.Log("Started");

        LoadSceneServerRpc("Scene1", LoadSceneMode.Single);
        
    }

    void ConnectToServer2Clicked()
    {
        NetworkManager.Singleton.Shutdown();
        //StartCoroutine(WaitForShutdown());
        Debug.Log("Before Setting a Connection");
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("127.0.0.1", 7778, "0.0.0.0");
        Debug.Log("After Setting a Connection");
        NetworkManager.Singleton.StartClient();
        LoadSceneServerRpc("Scene2", LoadSceneMode.Single);
        
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

    IEnumerator WaitForShutdown()
    {
        yield return new WaitForSeconds(1f);
    }
}
