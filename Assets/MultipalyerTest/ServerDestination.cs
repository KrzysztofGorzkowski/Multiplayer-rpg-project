using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ServerDestination
{
    private const int ServerPort = 7778;

    private TcpListener listener;
    private TcpClient client;
    private NetworkStream stream;

    public void StartListening()
    {
        listener = new TcpListener(IPAddress.Any, ServerPort);
        listener.Start();

        Debug.Log("Serwer docelowy nas�uchuje na porcie " + ServerPort);

        client = listener.AcceptTcpClient();
        stream = client.GetStream();

        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string request = Encoding.ASCII.GetString(buffer, 0, bytesRead);

        // Przetw�rz ��danie od serwera �r�d�owego
        string response = ProcessRequest(request);

        // Odpowiedz serwerowi �r�d�owemu
        byte[] data = Encoding.ASCII.GetBytes(response);
        stream.Write(data, 0, data.Length);

        // Zamknij po��czenie
        stream.Close();
        client.Close();
        listener.Stop();
    }

    private string ProcessRequest(string request)
    {
        // Przyk�adowa logika przetwarzania ��dania
        if (request == "GET_PLAYER_COUNT")
        {
            int playerCount = GetPlayerCount();
            return playerCount.ToString();
        }

        return string.Empty;
    }

    private int GetPlayerCount()
    {
        // Przyk�adowa implementacja logiki zwracaj�cej liczb� graczy
        int playerCount = 10; // Pobierz rzeczywist� liczb� graczy
        return playerCount;
    }
}
