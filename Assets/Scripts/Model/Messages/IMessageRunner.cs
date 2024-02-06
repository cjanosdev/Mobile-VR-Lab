using System.Threading.Tasks;
using Model.Messages;


namespace Model.Messages
{
    public interface IMessageRunner<T>
    {
        /// <summary>
        ///     Run a command message from the server.
        /// </summary>
        /// <param name="message">The message to run.</param>
        /// <returns></returns>
        Task Run(Model.Message<T> message);
    }
}