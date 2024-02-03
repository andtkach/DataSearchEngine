using System.Threading.Channels;
using AutoMapper;
using Common;
using Common.Events;
using MassTransit;

namespace DataEngine.API.Processors
{
    public class PersonDeletedProcessor : IHostedService
    {
        private readonly Channel<StorePersonDelete> _channel;
        private readonly ILogger<PersonDeletedProcessor> _logger;
        private IServiceScopeFactory _serviceScopeFactory;
        private IMapper _mapper;

        public PersonDeletedProcessor(Channel<StorePersonDelete> channel, ILogger<PersonDeletedProcessor> logger, 
            IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            _channel = channel;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            IPublishEndpoint publishEndpoint =
                scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
                
            return Task.Factory.StartNew(async () =>
            {
                while (!_channel.Reader.Completion.IsCanceled)
                {
                    var message = await _channel.Reader.ReadAsync(cancellationToken);
                    var personDeletedEvent = _mapper.Map<PersonDeletedEvent>(message);
                    await publishEndpoint.Publish(personDeletedEvent, cancellationToken);
                    _logger.LogInformation($"Republished delete: {message.Id}");
                }
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
