using System.Threading.Tasks;

namespace Comb
{
    public interface ISearchClient
    {
        string Endpoint { get; set; }

        Task<SearchResult<T>> SearchAsync<T>();
    }
}