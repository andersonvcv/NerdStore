using MediatR;
using NerdStore.Core.Messages;

namespace NerdStore.Core.MediatR
{
    internal class MediatRHandler : IMediatRHandler
    {
        private readonly IMediator _mediator;

        public MediatRHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T mediatREvent) where T : Event
        {
            await _mediator.Publish(mediatREvent);
        }

        public async Task PublishMessage<T>(T message) where T : Message
        {
            await _mediator.Publish(message);
        }
    }
}
