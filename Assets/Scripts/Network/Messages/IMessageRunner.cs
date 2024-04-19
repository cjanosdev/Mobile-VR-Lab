using System.Threading.Tasks;
using Model.Messages;


namespace Model.Messages
{
    public interface IMessageRunner
    {
        /// <summary>
        ///     Run a command message from the server.
        /// </summary>
        /// <param name="message">The message to run.</param>
        /// <returns></returns>
        Task Run<T>(Model.Message<T> message) where T : MessageBase;
    }
}