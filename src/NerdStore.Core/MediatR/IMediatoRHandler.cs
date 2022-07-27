using NerdStore.Core.Messages;

namespace NerdStore.Core.MediatR
{
    public interface IMediatoRHandler
    {
        Task PublishEvent<T>(T mediatREvent) where T : Event;

        Task PublishMessage<T>(T message) where T : Message;

        Task<bool> PublishCommand<T>(T message) where T : Command;
    }
}
