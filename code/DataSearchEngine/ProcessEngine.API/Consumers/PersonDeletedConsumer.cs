using AutoMapper;
using Common.Events;
using MassTransit;
using ProcessEngine.API.Search;
using ProcessEngine.API.Utils;
using SearchEngine.Domain;

namespace ProcessEngine.API.Consumers
{
    public sealed class PersonDeletedConsumer : IConsumer<PersonDeletedEvent>
    {
        private readonly ILogger<PersonDeletedConsumer> _logger;
        private readonly ISearchMutatorRepository<Person> _repository;
        private readonly IMapper _mapper;

        public PersonDeletedConsumer(ILogger<PersonDeletedConsumer> logger, ISearchMutatorRepository<Person> repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<PersonDeletedEvent> context)
        {
            _logger.LogInformation("Person deleted event received: {Id}", context.Message.Id);

            var result = await _repository.Delete(context.Message.Id);
            if (result)
            {
                _logger.LogInformation("Person deleted event: {Id}", context.Message.Id);
            }
        }
    }
}
