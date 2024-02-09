using System;
using System.Threading.Tasks;
using Model.ChangeScene;
using Model.Focus;
using Model.Messages.Query;

namespace Model.Messages
{
    public class MessageRunner: IMessageRunner
    {
        private FocusRunner focusRunner;
        private ChangeSceneRunner changeSceneRunner;
        private QueryRunner queryRunner;
        
        /// <summary>
        ///     Worker used to run a command message.
        /// </summary>
        public MessageRunner()
        {
            this.queryRunner = new QueryRunner();
            this.changeSceneRunner = new ChangeSceneRunner();
            this.focusRunner = new FocusRunner();
        }

        public async Task Run<T>(Model.Message<T> message) where T: MessageBase
        {
            if (message == null)
            {
                return;
            }

            await this.InternalRunDispatchToMessageWorker(message);
        }

        private async Task InternalRunDispatchToMessageWorker<T>(Message<T> message)
        {
            switch (message.MessageType)
            {
                case MessageType.Query:
                    // TODO: implement me
                    // await this.queryRunner.Run(message)
                    break;
                case MessageType.ChangeScene:
                    // TODO: implement me
                    // await this.changeSceneRunner.Run(message)
                    break;
                case MessageType.Focus:
                    await this.focusRunner.Run(message as Message<FocusData>);
                    break;
                default:
                    throw new ArgumentException($"Message type {message.MessageType} was not recognized.");
            }
        }
    }
}