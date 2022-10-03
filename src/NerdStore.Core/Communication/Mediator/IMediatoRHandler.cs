using NerdStore.Core.Messages;
using NerdStore.Core.Messages.Notifications;

namespace NerdStore.Core.Communication.Mediator
{
    public interface IMediatoRHandler
    {
        Task PublishEvent<T>(T mediatREvent) where T : Event;

        Task PublishMessage<T>(T message) where T : Message;

        Task<bool> SendCommand<T>(T message) where T : Command;

        Task PublishNotification<T>(T message) where T : DomainNotification;
    }
}
