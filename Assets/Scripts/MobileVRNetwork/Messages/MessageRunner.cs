using System;
using System.Threading.Tasks;
using MobileVRNetwork.Messages.ChangeScene;
using MobileVRNetwork.Messages.Focus;
using MobileVRNetwork.Messages.Transparency;


namespace MobileVRNetwork.Messages
{
    public class MessageRunner
    {
        private FocusRunner focusRunner;
        private ChangeSceneRunner changeSceneRunner;
        // private QueryRunner queryRunner;
        private TransparencyRunner transparencyRunner;
        
        /// <summary>
        ///     Worker used to run a command message.
        /// </summary>
        public MessageRunner()
        {
            // this.queryRunner = new QueryRunner();
            this.changeSceneRunner = new ChangeSceneRunner(UnityMainThreadDispatcher.instance);
            this.focusRunner = new FocusRunner();
            this.transparencyRunner = new TransparencyRunner();
        }

        public async Task Run<T>(Message<T> message) where T: MessageBase
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
                    await this.changeSceneRunner.Run(message as Message<ChangeSceneData>);
                    break;
                case MessageType.Focus:
                    await this.focusRunner.Run(message as Message<FocusData>);
                    break;
                case MessageType.TransparencyMode:
                    await this.transparencyRunner.Run(message as Message<TransparencyData>);
                    break;
                default:
                    throw new ArgumentException($"Message type {message.MessageType} was not recognized.");
            }
        }
    }
}