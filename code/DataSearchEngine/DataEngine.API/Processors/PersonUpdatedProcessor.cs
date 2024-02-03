using System.Threading.Channels;
using AutoMapper;
using Common;
using Common.Events;
using MassTransit;

namespace DataEngine.API.Processors
{
    public class PersonUpdatedProcessor : IHostedService
    {
        private readonly Channel<StorePersonUpdate> _channel;
        private readonly ILogger<PersonUpdatedProcessor> _logger;
        private IServiceScopeFactory _serviceScopeFactory;
        private IMapper _mapper;

        public PersonUpdatedProcessor(Channel<StorePersonUpdate> channel, ILogger<PersonUpdatedProcessor> logger, 
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
                    var personUpdatedEvent = _mapper.Map<PersonUpdatedEvent>(message);
                    await publishEndpoint.Publish(personUpdatedEvent, cancellationToken);
                    _logger.LogInformation($"Republished update: {message.Id}");
                }
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
