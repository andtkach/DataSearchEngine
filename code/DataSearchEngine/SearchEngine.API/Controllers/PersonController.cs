using SearchEngine.Application.Interfaces;
using SearchEngine.Domain;
using Microsoft.AspNetCore.Mvc;

namespace SearchEngine.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IGenericRepo<Person> repo;

        public PersonController(IGenericRepo<Person> repo)
        {
            this.repo = repo;
        }

        [HttpPost("create")]
        public async Task<IEnumerable<string>> Create(IEnumerable<Person> persons)
        {
            var result = await repo.Index(persons);
            return result;
        }
        

        [HttpGet("get")]
        public async Task<Person> Read(string id)
        {
            var result = await repo.Get(id);
            return result;
        }

        [HttpPut("update")]
        public async Task<bool> Update(Person person, string id)
        {
            if (!id.Equals(person.Id)) return false;
            var result = await repo.Update(person, id);
            return result;
        }

        [HttpDelete("delete")]
        public async Task<bool> Delete(string id)
        {
            var result = await repo.Delete(id);
            return result;
        }

        [HttpGet("search")]
        public async Task<IEnumerable<Person>> Search(string term)
        {
            var result = await repo.Search(term);
            return result;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Person>> All()
        {
            var result = await repo.All();
            return result;
        }
    }
}