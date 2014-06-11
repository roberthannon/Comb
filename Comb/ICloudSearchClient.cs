﻿using System.Threading.Tasks;
using Comb.Documents;
using Comb.Searching;
using Comb.Searching.Responses;

namespace Comb
{
    public interface ICloudSearchClient
    {
        Task UpdateAsync(DocumentRequest request);

        // TODO: UpdateAsync overload taking a batch.

        Task<SearchResponse<T>> SearchAsync<T>(SearchRequest request);
    }
}