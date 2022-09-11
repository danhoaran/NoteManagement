using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NoteManagement.Core.Dtos;
using NoteManagement.Core.Interfaces;
using NoteManagement.Infrastructure.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagement.Infrastructure.Services
{
    public class NoteApiService : INoteApiService
    {
        private readonly string _serviceUrl;
        private readonly RestClient _restClient;
        //private readonly IOptionsSnapshot<ApiConfig> _apiConfig;

        public NoteApiService(IOptionsSnapshot<ApiConfig> apiConfig)
        {
            _serviceUrl = apiConfig.Value.Url;
            var restOptions = new RestClientOptions(_serviceUrl);
            restOptions.RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            _restClient = new RestClient(restOptions);
        }

        public async Task<NoteCreationResponseDto> CreateNote(NoteForCreationDto dto)
        {
            var response = await PostRequest<NoteCreationResponseDto>("/api/Notes", dto);
            return response;
        }

        public async Task<List<NoteForListingDto>> GetAllNotes()
        {
            var response = await GetRequest<List<NoteForListingDto>>($"/api/Notes");
            return response;
        }

        public async Task<NoteForHtmlDto> GetNoteForWebsite(int noteId)
        {
            var response = await GetRequest<NoteForHtmlDto>($"/api/Notes/{noteId}/html");
            return response;
        }

        private async Task<T> GetRequest<T>(string endpoint)
        {
            var req = new RestRequest(endpoint);
            var response = await _restClient.GetAsync<T>(req);
            return response;
        }

        private async Task<T?> PostRequest<T>(string endpoint, object jsonBody)
        {
            try
            {
                var req = new RestRequest(endpoint);
                req.AddJsonBody(jsonBody);
                var response = await _restClient.PostAsync<T>(req);
                return response;
            }
            catch
            {
                return default(T);
            }
        }



    }
}
