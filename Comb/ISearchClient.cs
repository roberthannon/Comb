using System.Threading.Tasks;
using Comb.Searching;
using Comb.Searching.Responses;

namespace Comb
{
    public interface ISearchClient
    {
        string Endpoint { get; set; }

        Task<SearchResponse<T>> SearchAsync<T>(SearchRequest request)
            where T : ISearchResult;
    }
}