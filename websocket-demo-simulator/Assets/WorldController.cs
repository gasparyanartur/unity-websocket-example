using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;

public class WorldController : MonoBehaviour
{
    private WebSocket _webSocket;
    private float _timeSinceLastSentMsg = 0;

    private async void Start()
    {
        _webSocket = new WebSocket("ws://127.0.0.1:8005");

        _webSocket.OnOpen += () => { Debug.Log("Connection open!"); };

        _webSocket.OnError += (e) => { Debug.Log("Error! " + e); };

        _webSocket.OnClose += (e) => { Debug.Log("Connection closed!"); };

        _webSocket.OnMessage += (bytes) =>
        {
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("Received OnMessage! (" + bytes.Length + " bytes) " + message);
        };

        await _webSocket.Connect();
    }

    private void Update()
    {
        if (_webSocket != null)
            UpdateWebSocket();
    }

    private void UpdateWebSocket()
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            if (Time.time - _timeSinceLastSentMsg > 3)
            {
                SendWebSocketMessage("Sending message :)");
                _timeSinceLastSentMsg = Time.time;
            }
        }
#if !UNITY_WEBGL || UNITY_EDITOR
        _webSocket?.DispatchMessageQueue();
#endif
    }

    private void SendWebSocketMessage(byte[] message)
    {
        _webSocket.Send(message);
    }

    private void SendWebSocketMessage(string message)
    {
        _webSocket.SendText(message);
    }
}