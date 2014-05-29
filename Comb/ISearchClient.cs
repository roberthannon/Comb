using System.Threading.Tasks;
using Comb.Queries;
using Comb.Responses;

namespace Comb
{
    public interface ISearchClient
    {
        string Endpoint { get; set; }

        Task<SearchResponse<T>> SearchAsync<T>(SearchQuery query)
            where T : ISearchResponse;
    }
}