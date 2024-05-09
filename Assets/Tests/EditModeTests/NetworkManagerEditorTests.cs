using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NetworkManagerEditorTests
{
     private NetworkManager _networkManager;
        private FakeNetworkStream _fakeStream;
        private FakeTcpClient _fakeClient;
    
        [SetUp]
        public void SetUp()
        {
            _networkManager = new GameObject().AddComponent<NetworkManager>();
            _fakeClient = new FakeTcpClient();
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _fakeStream = new FakeNetworkStream(socket);
    
            _fakeClient.SetStream(_fakeStream);
    
            // Inject fake TcpClient into NetworkManager
            typeof(NetworkManager)
                .GetField("_client", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(_networkManager, _fakeClient);
        }
    
        [Test]
        public async Task InitializeSocketsAsync_ConnectsToServer()
        {
            // Arrange
            var expectedServer = "192.168.1.11";
            var expectedPort = 15300;
            _fakeClient.SetConnectAsyncBehavior(() => Task.CompletedTask);
    
            // Act
            // Act
            Debug.Log("Calling InitializeSocketsAsync...");
            await _networkManager.InitializeSocketsAsync();
            Debug.Log("InitializeSocketsAsync completed.");
    
            // Assert
            Assert.IsTrue(_fakeClient.WasConnectCalled(expectedServer, expectedPort));
        }
    
        [Test]
        public async Task WriteMessageAsync_SendsCorrectData()
        {
            // Arrange
            var testMessage = "Test Message";
            var jsonData = Encoding.UTF8.GetBytes(testMessage);
            byte[] sizeBytes = BitConverter.GetBytes(jsonData.Length);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(sizeBytes);
    
            // Act
            await NetworkManager.WriteMessageAsync(_fakeStream, testMessage);
    
            // Assert
            CollectionAssert.AreEqual(sizeBytes, _fakeStream.WrittenData.GetRange(0, sizeBytes.Length).ToArray());
            CollectionAssert.AreEqual(jsonData, _fakeStream.WrittenData.GetRange(sizeBytes.Length, jsonData.Length).ToArray());
        }
    
        [Test]
        public void OnApplicationQuit_ClosesTcpClient()
        {
            // Act
            _networkManager.OnApplicationQuit();
    
            // Assert
            Assert.IsTrue(_fakeClient.WasCloseCalled());
        }
    }
    
    public class FakeTcpClient : TcpClient
    {
        private FakeNetworkStream _stream;
        private bool _closeCalled;
        private string _lastServer;
        private int _lastPort;
        private Func<Task> _connectAsyncBehavior;
    
        public void SetStream(FakeNetworkStream stream) => _stream = stream;
        public void SetConnectAsyncBehavior(Func<Task> connectBehavior) => _connectAsyncBehavior = connectBehavior;
        public FakeNetworkStream GetStream() => _stream;
    
        public new Task ConnectAsync(string hostname, int port)
        {
            _lastServer = hostname;
            _lastPort = port;
            return _connectAsyncBehavior?.Invoke() ?? Task.CompletedTask;
        }
    
        public bool WasConnectCalled(string server, int port) => _lastServer == server && _lastPort == port;
        public bool WasCloseCalled() => _closeCalled;
    
        public new void Close()
        {
            _closeCalled = true;
            base.Close();
        }
    }
    
    
    public class FakeNetworkStream : NetworkStream
    {
        private List<byte> _writtenData = new List<byte>();
        public List<byte> WrittenData => _writtenData;
    
        public bool CanWriteOverride { get; set; } = true;
    
        // Constructor requires a Socket
        public FakeNetworkStream(Socket socket)
            : base(socket, ownsSocket: true) // Assumes ownership of the socket for disposal
        {
        }
    
        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            _writtenData.AddRange(buffer.Skip(offset).Take(count));
            return Task.CompletedTask;
        }
    }
