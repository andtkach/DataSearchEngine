using Common;
using Nest;

namespace ProcessEngine.API.Search
{
    public class SearchMutatorRepository<T> : ISearchMutatorRepository<T> where T : class, IPerson
    {
        private readonly IElasticClient _client;

        public SearchMutatorRepository(IElasticClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<string>> Index(IEnumerable<T> documents)
        {
            var indexName = typeof(T).Name.ToLower();
            var indexResponse = await _client.Indices.ExistsAsync(indexName);

            if (!indexResponse.Exists)
            {
                var newIndex = await _client.Indices.CreateAsync(indexName, i => i.Map<T>(x => x.AutoMap()));
            }

            var response = await _client.IndexManyAsync(documents, indexName);
            return response.Items.Select(x => x.Id);
        }

        public async Task<bool> Update(T document, string id)
        {
            var indexName = typeof(T).Name.ToLower();
            var result = await _client.UpdateAsync<T>(id, u => u.Doc(document).Index(indexName));
            return result.IsValid;
        }

        public async Task<bool> Delete(string id)
        {
            var indexName = typeof(T).Name.ToLower();
            var result = await _client.DeleteAsync<T>(id, i => i.Index(indexName));
            return result.IsValid;
        }
    }
}
