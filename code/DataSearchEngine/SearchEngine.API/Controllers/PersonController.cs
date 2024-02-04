using SearchEngine.Application.Interfaces;
using SearchEngine.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Nest;
using System.Text.Json;

namespace SearchEngine.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IGenericSearchRepository<Person> _repository;
        private readonly IDistributedCache _cache;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IGenericSearchRepository<Person> repository, IDistributedCache cache, ILogger<PersonController> logger)
        {
            _repository = repository;
            _cache = cache;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IEnumerable<string>> Create(IEnumerable<Person> persons)
        {
            var result = await _repository.Index(persons);
            return result;
        }
        

        [HttpGet("get")]
        public async Task<Person> Read(string id)
        {
            var result = await _repository.Get(id);
            return result;
        }

        [HttpPut("update")]
        public async Task<bool> Update(Person person, string id)
        {
            if (!id.Equals(person.Id)) return false;
            var result = await _repository.Update(person, id);
            return result;
        }

        [HttpDelete("delete")]
        public async Task<bool> Delete(string id)
        {
            var result = await _repository.Delete(id);
            return result;
        }

        [HttpGet("search")]
        public async Task<IEnumerable<Person>> Search(string term)
        {
            var cached = await _cache.GetStringAsync($"search-{term}");
            if (!string.IsNullOrEmpty(cached))
            {
                _logger.LogInformation("Get search result from cache for term: {term}", term);
                return JsonSerializer.Deserialize<List<Person>>(cached) ?? []; 
            }

            _logger.LogInformation("Get search result from server and populate cache for term: {term}", term);
            var result = (await _repository.Search(term)).ToList();
            await _cache.SetStringAsync($"search-{term}", JsonSerializer.Serialize(result), CacheOptions.DefaultExpiration);

            return result;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Person>> All()
        {
            var result = await _repository.All();
            return result;
        }

        [HttpGet("count")]
        public async Task<int> Count()
        {
            var result = await _repository.Count();
            return result;
        }
    }
}