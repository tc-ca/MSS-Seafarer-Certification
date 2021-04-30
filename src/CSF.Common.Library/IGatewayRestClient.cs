using System.Threading.Tasks;

namespace CSF.Common.Library
{
    public interface IGatewayRestClient
    {
        Task<TReturnMessage> GetAsync<TReturnMessage>(ServiceLocatorDomain serviceName, string path) where TReturnMessage : class, new();
    }
}