namespace Threadly.UI.Services.Abstracts
{
    public interface IApiService
    {
        Task<TResult> GetAsync<TData,TResult>(string endpoint);
        Task<TResult> PostAsync<TData, TResult>(string endpoint, TData data);
        Task<TResult> PostStreamAsync<TResult>(string endpoint, MultipartFormDataContent data);
        Task<TResult> PutAsync<TData, TResult>(string endpoint, TData data);
        Task<TResult> DeleteAsync<TData, TResult>(string endpoint);
        
    }
}
