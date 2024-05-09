using System.Net.Sockets;
using System.Threading.Tasks;
using MobileVRNetwork.Messages;

namespace DefaultNamespace
{
    public interface INetworkManager
    {
        Task InitializeSocketsAsync();
        Task InitialConnectionAsync(NetworkStream stream);
        void AsyncReadingLogic(NetworkStream stream);
        Task ReceiveCommandsAsync(NetworkStream networkStream);
        Task<MessageResponse> ProcessMessage<T>(Message<T> message);
    }
}