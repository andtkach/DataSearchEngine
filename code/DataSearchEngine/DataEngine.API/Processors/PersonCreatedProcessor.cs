using System.Threading.Channels;
using AutoMapper;
using Common;
using Common.Events;
using MassTransit;

namespace DataEngine.API.Processors
{
    public class PersonCreatedProcessor : IHostedService
    {
        private readonly Channel<StorePersonCreate> _channel;
        private readonly ILogger<PersonCreatedProcessor> _logger;
        private IServiceScopeFactory _serviceScopeFactory;
        private IMapper _mapper;

        public PersonCreatedProcessor(Channel<StorePersonCreate> channel, ILogger<PersonCreatedProcessor> logger, 
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
                    var personCreatedEvent = _mapper.Map<PersonCreatedEvent>(message);
                    await publishEndpoint.Publish(personCreatedEvent, cancellationToken);
                    _logger.LogInformation($"Republished create: {message.Id}");
                }
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
