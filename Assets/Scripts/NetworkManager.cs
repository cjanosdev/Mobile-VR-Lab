using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ChangeScene;
using Model.Focus;
using Model.InitialConnection;
using Model.Messages;
using Model.Messages.Query;
using Newtonsoft.Json;
using UnityEngine;


public class NetworkManager : MonoBehaviour
{
    private TcpClient _client;
    private NetworkStream _stream;
    private IMessageRunner _messageRunner;
    
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeBeforeSceneLoad()
    {
        
        var networkManager = FindObjectOfType<NetworkManager>();
        if (networkManager != null) return;
        networkManager = new GameObject("NetworkManager").AddComponent<NetworkManager>();
        DontDestroyOnLoad(networkManager.gameObject);

    }

    private async void Start()
    {
        this._messageRunner = new MessageRunner();
        await InitializeSocketsAsync();
    }

    private async Task InitializeSocketsAsync()
    {
        try
        {
            _client = new TcpClient();
            await _client.ConnectAsync("192.168.1.22", 15300);
            _stream = _client.GetStream();

            await InitialConnectionAsync(_stream);
        } 
        catch (SocketException ex)
        {
            Debug.LogError("Socket error connecting to server: " + ex.Message);
        }
        catch (Exception e)
        {
            Debug.LogError("Error connecting to server: " + e.Message);
        }
    }

    private static async Task WriteMessageAsync(NetworkStream stream, string jsonString)
    {
        // Serialize the message object to JSON
        byte[] jsonData = Encoding.UTF8.GetBytes(jsonString);

        // Write the size of the JSON data as preamble (big-endian)
        byte[] sizeBytes = BitConverter.GetBytes(jsonData.Length);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(sizeBytes); // Convert to big-endian if necessary
        }
        await stream.WriteAsync(sizeBytes, 0, sizeBytes.Length);

        // Write the JSON data
        await stream.WriteAsync(jsonData, 0, jsonData.Length);
    }

    private static async Task<T> ReadMessageAsync<T>(NetworkStream stream)
    {
        // Buffer to read the preamble containing the size of the JSON message
        var sizeBuffer = new byte[4]; // Assuming the size is encoded in a 4-byte integer

        // Read the preamble
        await stream.ReadAsync(sizeBuffer, 0, sizeBuffer.Length);

        // Convert the size bytes to an integer
        var messageSize = BitConverter.ToInt32(sizeBuffer, 0);

        // Buffer to read the JSON message
        var messageBuffer = new byte[messageSize];

        // Read the JSON message
        await stream.ReadAsync(messageBuffer, 0, messageSize);

        // Convert the message bytes to a string
        var jsonString = Encoding.UTF8.GetString(messageBuffer);

        // Deserialize the JSON message into the specified object type
        return JsonConvert.DeserializeObject<T>(jsonString);
    }

    private async Task InitialConnectionAsync(NetworkStream stream)
    {
        
        // read
        var version = await ReadMessageAsync<InitialConnection>(stream);
        
        if (version != null)
        {
            // Sends headset ID back to Server.
            var id = GenerateStringID();
            
            var response = new InitialResponse
            {
                ID = id
            };

            // Serialize the response object to JSON
            var json = JsonConvert.SerializeObject(response);
        
            // Send length-prefixed JSON
            await WriteMessageAsync(stream, json);
        
            // Wait for confirmation response
            var confirmation = await ReadMessageAsync<InitialConfirmation>(stream);
            if (confirmation is { SessionName: not null })
            {
                await ReceiveCommandsAsync(stream);
            }
        }
    }
    

    private async Task ReceiveCommandsAsync(NetworkStream networkStream)
    {
            while (_client.Connected)
            {
                // Deserialize the JSON message into the specified object type
                var message = await ReadMessageAsync<Message<dynamic>>(networkStream);
                if (message == null)
                    continue;

                // Offload processing to a background thread
                _ = Task.Run(async () =>
                {
                    try
                    {
                        var response = await ProcessMessage(message);
                        var json = JsonConvert.SerializeObject(response);
                        
                        // Send length-prefixed JSON
                        await WriteMessageAsync(networkStream, json);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError("Error processing message: " + ex.Message);
                    }
                });
            }
    }
    
    private async Task<MessageResponse> ProcessMessage<T>(Message<T> message) 
    {
        switch (message.MessageType)
        {
            case MessageType.Query:
                var queryData = JsonConvert.DeserializeObject<QueryData>(message.Data.ToString());
                var queryMessage = new Message<QueryData>{ MessageType = message.MessageType, Data = queryData,};
                await this._messageRunner.Run(queryMessage);
                return new MessageResponse()
                {
                    Error = false,
                    Response = null,
                };
               // break;
            case MessageType.ChangeScene:
                var changeSceneData = JsonConvert.DeserializeObject<ChangeSceneData>(message.Data.ToString());
                var changeSceneMessage = new Message<ChangeSceneData>{ MessageType = message.MessageType, Data = changeSceneData};
                await this._messageRunner.Run(changeSceneMessage);
                return new MessageResponse()
                {
                    Error = false,
                    Response = null,
                };
               // break;
            case MessageType.Focus:
                var focusData = JsonConvert.DeserializeObject<FocusData>(message.Data.ToString());
                var focusMessage = new Message<FocusData>{MessageType = message.MessageType, Data = focusData};
                await this._messageRunner.Run(focusMessage);
                // temporary just to test connection with server will send a positive response:
                return new MessageResponse()
                {
                    Error = false,
                    Response = null,
                };
            default:
                throw new ArgumentException("Unsupported message type: " + message.MessageType);
        }
    }

    private static string GenerateStringID()
    {
        return "abcdefgh";
    }

    private void OnApplicationQuit()
    {
        _client?.Close();
    }
}
