using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comb
{
    public interface ICloudSearchClient
    {
        Task<UpdateResponse> UpdateAsync(DocumentRequest request);
        Task<UpdateResponse> UpdateAsync(IEnumerable<DocumentRequest> requests);

        Task<SearchResponse<T>> SearchAsync<T>(SearchRequest request);
    }
}