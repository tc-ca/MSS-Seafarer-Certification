namespace CDNApplication.Utilities
{
    using System.Threading.Tasks;

    public interface IRestClient
    {
        Task<TReturnMessage> GetAsync<TReturnMessage>(ServiceDomain serviceName, string path)
            where TReturnMessage : class, new();

        Task<TReturnMessage> PostAsync<TReturnMessage>(ServiceDomain serviceName, string path, object dataObject = null)
            where TReturnMessage : class, new();

        Task<TReturnMessage> PutAsync<TReturnMessage>(ServiceDomain serviceName, string path, object dataObject = null)
            where TReturnMessage : class, new();

        Task<bool> DeleteAsync(ServiceDomain serviceName, string path);
    }
}