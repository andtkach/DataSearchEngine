using SearchEngine.Domain;
using SearchEngine.Application.Interfaces;
using Nest;

namespace SearchEngine.Persistence
{
    public class GenericSearchRepo<T> : IGenericRepo<T> where T : class, IPerson
    {
        private readonly IElasticClient _client;

        public GenericSearchRepo(IElasticClient client)
        {
            this._client = client;
        }

        public async Task<bool> Delete(string id)
        {
            var indexName = typeof(T).Name.ToLower();
            var result = await _client.DeleteAsync<T>(id, i => i.Index(indexName));
            return result.IsValid;
        }

        public async Task<IEnumerable<T>> Search(string term)
        {
            var indexName = typeof(T).Name.ToLower();
            var response = await _client.SearchAsync<T>(s => s
                .Index(indexName)
                .From(0)
                .Size(10)
                .Query(q => q
                    .MultiMatch(m => m
                        .Fields(fs => fs
                            .Field(f => f.FirstName)
                            .Field(f => f.LastName)
                            .Field(f => f.Email)
                            .Field(f => f.EmailText)
                            .Field(f => f.Country)
                            .Field(f => f.Dob)
                            .Field(f => f.Bio)
                        )
                        .Type(TextQueryType.PhrasePrefix)
                        .Query(term)
                    )
                )
            );

            if (response.IsValid)
            {
                var doc = response.Documents.ToList();
                return doc;
            }

            return new List<T>();
        }

        public async Task<IEnumerable<T>> All()
        {
            var indexName = typeof(T).Name.ToLower();
            var response = await _client.SearchAsync<T>(s => s
                .Index(indexName)
                .From(0)
                .Size(10)
            );

            if (!response.IsValid) return new List<T>();
            return response.Documents.ToList();
        }

        public async Task<T> Get(string id)
        {
            var indexName = typeof(T).Name.ToLower();
            var result = await _client.GetAsync<T>(id, i => i.Index(indexName));
            return result.Source;
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
    }
}