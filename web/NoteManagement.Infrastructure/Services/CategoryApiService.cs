using Microsoft.Extensions.Options;
using NoteManagement.Core.Dtos;
using NoteManagement.Core.Interfaces;
using NoteManagement.Infrastructure.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace NoteManagement.Infrastructure.Services
{
    public class CategoryApiService : ICategoryApiService
    {
        private readonly string _serviceUrl;
        private readonly RestClient _restClient;

        public CategoryApiService()
        {
            _serviceUrl = "https://localhost:7105/";
            var restOptions = new RestClientOptions(_serviceUrl);
            restOptions.RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            _restClient = new RestClient(restOptions);
        }

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var response = await GetRequest<List<CategoryDto>>($"/api/Categories");
            return response;
        }

        private async Task<T> GetRequest<T>(string endpoint)
        {
            var req = new RestRequest(endpoint);
            var response = await _restClient.GetAsync<T>(req);
            return response;
        }

    }
}
