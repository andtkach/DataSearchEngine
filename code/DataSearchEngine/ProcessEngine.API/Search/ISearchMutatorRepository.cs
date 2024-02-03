namespace ProcessEngine.API.Search
{
    public interface ISearchMutatorRepository<T>
    {
        Task<IEnumerable<string>> Index(IEnumerable<T> documents);
        Task<bool> Update(T document, string id);
        Task<bool> Delete(string id);

    }
}
