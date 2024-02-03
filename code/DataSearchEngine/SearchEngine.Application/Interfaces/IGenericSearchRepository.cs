namespace SearchEngine.Application.Interfaces
{
    public interface IGenericSearchRepository<T>
    {
        Task<IEnumerable<string>> Index(IEnumerable<T> documents);
        Task<T> Get(string id);
        Task<bool> Update(T document, string id);
        Task<bool> Delete(string id);
        Task<IEnumerable<T>> Search(string term);
        Task<IEnumerable<T>> All();
        Task<int> Count();
    }
}
