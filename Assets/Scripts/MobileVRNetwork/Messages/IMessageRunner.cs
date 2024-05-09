using System.Threading.Tasks;


namespace MobileVRNetwork.Messages
{
    public interface IMessageRunner
    {
        /// <summary>
        ///     Run a command message from the server.
        /// </summary>
        /// <param name="message">The message to run.</param>
        /// <returns></returns>
        Task Run<T>(Message<T> message) where T : MessageBase;
    }
}