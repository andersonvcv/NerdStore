using NerdStore.Core.Messages;

namespace NerdStore.Core.MediatR
{
    public interface IMediatRHandler
    {
        Task PublishEvent<T>(T mediatREvent) where T : Event;

        Task PublishMessage<T>(T message) where T : Message;
    }
}
