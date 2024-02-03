using AutoMapper;
using Common.Events;
using MassTransit;
using ProcessEngine.API.Search;
using ProcessEngine.API.Utils;
using SearchEngine.Domain;

namespace ProcessEngine.API.Consumers
{
    public sealed class PersonCreatedConsumer : IConsumer<PersonCreatedEvent>
    {
        private readonly ILogger<PersonCreatedConsumer> _logger;
        private readonly ISearchMutatorRepository<Person> _repository;
        private readonly IMapper _mapper;

        public PersonCreatedConsumer(ILogger<PersonCreatedConsumer> logger, ISearchMutatorRepository<Person> repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<PersonCreatedEvent> context)
        {
            _logger.LogInformation("Person created event received: {Id}", context.Message.Id);

            var p = _mapper.Map<Person>(context.Message);
            p.EmailText = EmailSplitter.SplitEmail(context.Message.Email);

            var listWithOnePerson = new List<Person> { p };
            var result = await _repository.Index(listWithOnePerson);
            var enumerable = result as string[] ?? result.ToArray();
            if (enumerable.Any())
            {
                _logger.LogInformation("Person created event indexed: {Id}", enumerable.FirstOrDefault());
            }
        }
    }
}
