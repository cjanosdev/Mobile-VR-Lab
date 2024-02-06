using System;
using System.Threading.Tasks;
using Model.Messages;

namespace Model.Messages
{
    public class MessageRunner<T> : IMessageRunner<T>
    {
        /// <summary>
        ///     Worker used to run a command message.
        /// </summary>
        public MessageRunner()
        {
            
        }

        public async Task Run(Model.Message<T> message)
        {
            if (message == null)
            {
                return;
            }

            await this.InternalRunDispatchToMessageWorker(message);
        }

        private async Task InternalRunDispatchToMessageWorker(Message<T> message)
        {
            switch (message.MessageType)
            {
                case MessageType.Query:
                    // await this.RunQuery(message)
                    break;
                case MessageType.ChangeScene:
                    // await this.RunChangeScene(message)
                    break;
                case MessageType.Focus:
                    // await this.RunFocus(message)
                    break;
                default:
                    throw new ArgumentException($"Message type {message.MessageType} was not recognized.");
            }
        }
    }
}