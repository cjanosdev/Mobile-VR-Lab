using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Messages;
using Newtonsoft.Json;
using UnityEngine;


public class NetworkManager<T> : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    private IMessageRunner<T> messageRunner;

    public NetworkManager(IMessageRunner<T> messageRunner)
    {
        this.messageRunner = messageRunner;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeBeforeSceneLoad()
    {
        var networkManager = new GameObject("NetworkManager").AddComponent<NetworkManager<T>>();
        DontDestroyOnLoad(networkManager.gameObject);
    }

    private async void Start()
    {
        await InitializeSocketsAsync();
    }

    private async Task InitializeSocketsAsync()
    {
        try
        {
            client = new TcpClient();
            await client.ConnectAsync("127.0.0.1", 15300);
            stream = client.GetStream();

            await ReceiveCommandsAsync();
        }
        catch (Exception e)
        {
            Debug.LogError("Error connecting to server: " + e.Message);
        }
    }

    private async Task ReceiveCommandsAsync()
    {
        using (StreamReader reader = new StreamReader(stream, Encoding.ASCII))
        {
            while (client.Connected)
            {
                string json = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(json))
                    continue;

                try
                {
                    var message = JsonConvert.DeserializeObject<Message<T>>(json);
                    await this.messageRunner.Run(message);
                }
                catch (JsonException ex)
                {
                    Debug.LogError("Error parsing JSON: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error processing message: " + ex.Message);
                }
            }
        }
    }

    private async void OnApplicationQuit()
    {
        client?.Close();
    }
}
