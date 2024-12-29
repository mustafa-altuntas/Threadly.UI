using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Threadly.UI.DTOs;
using Threadly.UI.Models.ViewModels.Community;
using Threadly.UI.Services.Abstracts;

namespace Threadly.UI.Services.Concretes
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TResult> GetAsync<TData, TResult>(string endpoint)
        {
            var respose = await _httpClient.GetAsync(endpoint);
            respose.EnsureSuccessStatusCode();
            var resposeContent = await respose.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResult>(resposeContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // property büyük/küçük harf uyumsuz
                WriteIndented = true
            })!;

        }

        public async Task<TResult> PostAsync<TData, TResult>(string endpoint, TData data)
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, jsonContent);

            if(response.IsSuccessStatusCode)
            {
                var resultmessage = await response.Content.ReadAsStringAsync();
            }
            else
            {
                var resultmessage = await response.Content.ReadAsStringAsync();
            }

            response.EnsureSuccessStatusCode();



            var resposeContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResult>(resposeContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // property büyük/küçük harf uyumsuz
                WriteIndented = true
            })!;


            //var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            //var response = await _httpClient.PostAsync(endpoint, content);
            //response.EnsureSuccessStatusCode();
            //var responseContent = await response.Content.ReadAsStringAsync();
            //return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }

        public async Task<TResult> PutAsync<TData, TResult>(string endpoint, TData data)
        {
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResult>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // property büyük/küçük harf uyumsuz
                WriteIndented = true
            })!;
        }

        public async Task<TResult> DeleteAsync<TData, TResult>(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResult>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // property büyük/küçük harf uyumsuz
                WriteIndented = true
            })!;
        }

        public async Task<TResult> PostStreamAsync<TResult>(string endpoint, MultipartFormDataContent data)
        {
            var response = await _httpClient.PostAsync(endpoint, data);
            response.EnsureSuccessStatusCode();

            var resposeContent = await response.Content.ReadAsStringAsync();


            return JsonSerializer.Deserialize<TResult>(resposeContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // property büyük/küçük harf uyumsuz
                WriteIndented = true
            })!;

        }
    }
}
