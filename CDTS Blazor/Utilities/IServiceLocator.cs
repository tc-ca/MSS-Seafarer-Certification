namespace CDNApplication.Utilities
{
    public interface IServiceLocator
    {
        string GetServiceUri(ServiceDomain serviceName);
    }
}