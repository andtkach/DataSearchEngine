using AutoMapper;
using Common.Events;
using MassTransit;
using ProcessEngine.API.Search;
using ProcessEngine.API.Utils;
using SearchEngine.Domain;

namespace ProcessEngine.API.Consumers
{
    public sealed class PersonUpdatedConsumer : IConsumer<PersonUpdatedEvent>
    {
        private readonly ILogger<PersonUpdatedConsumer> _logger;
        private readonly ISearchMutatorRepository<Person> _repository;
        private readonly IMapper _mapper;

        public PersonUpdatedConsumer(ILogger<PersonUpdatedConsumer> logger, ISearchMutatorRepository<Person> repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<PersonUpdatedEvent> context)
        {
            _logger.LogInformation("Person updated event received: {Id}", context.Message.Id);

            var p = _mapper.Map<Person>(context.Message);
            p.EmailText = EmailSplitter.SplitEmail(context.Message.Email);

            
            var result = await _repository.Update(p, context.Message.Id);
            if (result)
            {
                _logger.LogInformation("Person updated event: {Id}", context.Message.Id);
            }
        }
    }
}
