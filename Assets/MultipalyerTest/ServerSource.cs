using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ServerSource
{
    private const string ServerAddress = "127.0.0.1";
    private const int ServerPort = 7777;

    private TcpClient client;
    private NetworkStream stream;

    public void ConnectToTargetServer()
    {
        client = new TcpClient();
        client.Connect(ServerAddress, ServerPort);

        stream = client.GetStream();

        // Przyk�adowa wiadomo�� ��dania
        string message = "GET_PLAYER_COUNT";
        byte[] data = Encoding.ASCII.GetBytes(message);

        stream.Write(data, 0, data.Length);

        // Odbierz odpowied� od serwera docelowego
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

        // Przetw�rz otrzyman� odpowied�
        int playerCount = int.Parse(response);
        Debug.Log("Liczba graczy na serwerze docelowym: " + playerCount);

        // Zamknij po��czenie
        stream.Close();
        client.Close();
    }
}
