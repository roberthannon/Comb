using System.Collections.Generic;
using System.Threading.Tasks;
using Comb.Documents;
using Comb.Searching;
using Comb.Searching.Responses;

namespace Comb
{
    public interface ICloudSearchClient
    {
        Task UpdateAsync(DocumentRequest request);
        Task UpdateAsync(IEnumerable<DocumentRequest> requests);

        Task<SearchResponse<T>> SearchAsync<T>(SearchRequest request);
    }
}