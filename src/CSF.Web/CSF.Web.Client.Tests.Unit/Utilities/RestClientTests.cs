namespace CSF.Web.Client.Tests.Unit.Utilities
{
    using CSF.Common.Library;
    using CSF.Web.Client.Data.Services;
    using CSF.Web.Client.Utilities;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using Moq.Protected;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text.Json.Serialization;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class RestClientTests
    {
        private readonly Mock<IConfiguration> configuration;
        private readonly Mock<IServiceLocator> serviceLocator;
        private readonly Mock<IKeyVaultService> keyValutService;

        public RestClientTests()
        {
            this.configuration = this.buildMockConfiguration();
            this.serviceLocator = this.buildMockServiceLocator();
            this.keyValutService = this.buildKeyVaultService();
        }

        [Fact]
        public void RestClient_GetAsync_ReturnsResult()
        {
            // Arrange
            var handlerMock = this.buildHandlerMock(HttpStatusCode.OK, "{ 'Id':1,'Name':'John Wick'}");
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://localhost/MTAPI-INT"),
            };

            var restClient = new UnauthenticatedRestClient(httpClient, this.serviceLocator.Object);
            var expectedUri = new Uri("https://localhost/MTAPI-INT/apiTest");
            var expectedName = "John Wick";
            var expectedId = 1;

            // Act
            var result = restClient.GetAsync<ResponseObject>(ServiceLocatorDomain.Mtoa, "apiTest");

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Exception);
            Assert.Equal(expectedId, result.Result.Id);
            Assert.Equal(expectedName, result.Result.Name);
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1), // we expected a single external request
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Get  // we expected a GET request
                  && req.RequestUri == expectedUri // to this uri
               ),
               ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public void RestClient_GetAsync_ThrowsHttpRequestException()
        {
            // Arrange
            var handlerMock = this.buildHandlerMock(HttpStatusCode.BadRequest, "{ 'Id':1,'Name':'John Wick'}");
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://localhost/MTAPI-INT"),
            };

            var restClient = new UnauthenticatedRestClient(httpClient, this.serviceLocator.Object);
            var expectedUri = new Uri("https://localhost/MTAPI-INT/apiTest");
            var expectedStatusCode = HttpStatusCode.BadRequest;

            // Act
            var result = restClient.GetAsync<ResponseObject>(ServiceLocatorDomain.Mtoa, "apiTest");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Exception);
            Assert.Equal(result.Exception.InnerException.Data["StatusCode"], expectedStatusCode);
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1), // we expected a single external request
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Get  // we expected a GET request
                  && req.RequestUri == expectedUri // to this uri
               ),
               ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public void RestClient_PostAsync_ReturnsResult()
        {
            // Arrange
            var handlerMock = this.buildHandlerMock(HttpStatusCode.OK, "{ 'Id':1,'Name':'John Wick'}");
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://localhost/MTAPI-INT"),
            };

            var restClient = new UnauthenticatedRestClient(httpClient, this.serviceLocator.Object);
            var expectedUri = new Uri("https://localhost/MTAPI-INT/apiTest");
            var expectedName = "John Wick";
            var expectedId = 1;

            // Act
            var result = restClient.PostAsync<ResponseObject>(ServiceLocatorDomain.Mtoa, "apiTest");

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Exception);
            Assert.Equal(expectedId, result.Result.Id);
            Assert.Equal(expectedName, result.Result.Name);
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1), // we expected a single external request
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Post  // we expected a POST request
                  && req.RequestUri == expectedUri // to this uri
               ),
               ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public void RestClient_PostAsync_ThrowsHttpRequestException()
        {
            // Arrange
            var handlerMock = this.buildHandlerMock(HttpStatusCode.BadRequest, "{ 'Id':1,'Name':'John Wick'}");
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://localhost/MTAPI-INT"),
            };

            var restClient = new UnauthenticatedRestClient(httpClient, this.serviceLocator.Object);
            var expectedUri = new Uri("https://localhost/MTAPI-INT/apiTest");
            var expectedStatusCode = HttpStatusCode.BadRequest;

            // Act
            var result = restClient.PostAsync<ResponseObject>(ServiceLocatorDomain.Mtoa, "apiTest");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Exception);
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1), // we expected a single external request
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Post  // we expected a POST request
                  && req.RequestUri == expectedUri // to this uri
               ),
               ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public void RestClient_PutAsync_ReturnsResult()
        {
            // Arrange
            var handlerMock = this.buildHandlerMock(HttpStatusCode.OK, "{ 'Id':1,'Name':'John Wick'}");
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://localhost/MTAPI-INT"),
            };

            var restClient = new UnauthenticatedRestClient(httpClient, this.serviceLocator.Object);
            var expectedUri = new Uri("https://localhost/MTAPI-INT/apiTest");
            var expectedName = "John Wick";
            var expectedId = 1;

            // Act
            var result = restClient.PutAsync<ResponseObject>(ServiceLocatorDomain.Mtoa, "apiTest");

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Exception);
            Assert.Equal(expectedId, result.Result.Id);
            Assert.Equal(expectedName, result.Result.Name);
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1), // we expected a single external request
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Put  // we expected a PUT request
                  && req.RequestUri == expectedUri // to this uri
               ),
               ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public void RestClient_PutAsync_ThrowsHttpRequestException()
        {
            // Arrange
            var handlerMock = this.buildHandlerMock(HttpStatusCode.BadRequest, "{ 'Id':1,'Name':'John Wick'}");
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://localhost/MTAPI-INT"),
            };

            var restClient = new UnauthenticatedRestClient(httpClient, this.serviceLocator.Object);
            var expectedUri = new Uri("https://localhost/MTAPI-INT/apiTest");
            var expectedStatusCode = HttpStatusCode.BadRequest;

            // Act
            var result = restClient.PutAsync<ResponseObject>(ServiceLocatorDomain.Mtoa, "apiTest");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Exception);
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1), // we expected a single external request
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Put  // we expected a PUT request
                  && req.RequestUri == expectedUri // to this uri
               ),
               ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public void RestClient_DeleteAsync_Succeeds()
        {
            // Arrange
            var handlerMock = this.buildHandlerMock(HttpStatusCode.OK, string.Empty);
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://localhost/MTAPI-INT"),
            };

            var restClient = new UnauthenticatedRestClient(httpClient, this.serviceLocator.Object);
            var expectedUri = new Uri("https://localhost/MTAPI-INT/apiTest");
            var expectedSuccess = true;

            // Act
            var result = restClient.DeleteAsync(ServiceLocatorDomain.Mtoa, "apiTest");

            // Assert
            Assert.Equal(expectedSuccess, result.Result);
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1), // we expected a single external request
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Delete  // we expected a DELETE request
                  && req.RequestUri == expectedUri // to this uri
               ),
               ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public void RestClient_DeleteAsync_Fails()
        {
            // Arrange
            var handlerMock = this.buildHandlerMock(HttpStatusCode.BadRequest, string.Empty);
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://localhost/MTAPI-INT"),
            };

            var restClient = new UnauthenticatedRestClient(httpClient, this.serviceLocator.Object);
            var expectedUri = new Uri("https://localhost/MTAPI-INT/apiTest");
            var expectedSuccess = false;

            // Act
            var result = restClient.DeleteAsync(ServiceLocatorDomain.Mtoa, "apiTest");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Exception);
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1), // we expected a single external request
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Delete  // we expected a DELETE request
                  && req.RequestUri == expectedUri // to this uri
               ),
               ItExpr.IsAny<CancellationToken>()
            );
        }

        public Mock<IConfiguration> buildMockConfiguration()
        {
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(config => config.GetSection("AzureKeyVaultSettings:SecretNames")["MtoaApiKey"]).Returns("MtoaApiKey");
            configuration.Setup(config => config.GetSection("AzureKeyVaultSettings:SecretNames")["MtoaJwtToken"]).Returns("MtoaJwtToken");
            return configuration;
        }

        public Mock<IServiceLocator> buildMockServiceLocator()
        {
            var serviceLocator = new Mock<IServiceLocator>();
            serviceLocator.Setup(service => service.GetServiceUri(ServiceLocatorDomain.Mtoa)).Returns(new Uri("https://localhost/MTAPI-INT"));
            return serviceLocator;
        }

        public Mock<IKeyVaultService> buildKeyVaultService() {
            var keyValutService = new Mock<IKeyVaultService>();
            keyValutService.Setup(keyvault => keyvault.GetSecretByName("MtoaApiKey")).Returns("MtoaApiKey");
            keyValutService.Setup(keyvault => keyvault.GetSecretByName("MtoaJwtToken")).Returns("MtoaJwtToken");
            return keyValutService;
        }

        public Mock<HttpMessageHandler> buildHandlerMock(HttpStatusCode statusCode, string content)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = statusCode,
                   Content = new StringContent(content),
               })
               .Verifiable();

            return handlerMock;
        }

        private class ResponseObject
        {
            [JsonPropertyName("Name")]
            public string Name { get; set; }

            [JsonPropertyName("Id")]
            public int Id { get; set; }
        }


        /*
            public async Task<TReturnMessage> PutAsync<TReturnMessage>(ServiceLocatorDomain serviceName, string path, object dataObject = null)
                where TReturnMessage : class, new()
            {
                var uri = new Uri($"{this.serviceLocator.GetServiceUri(serviceName)}/{path}");

                var content = dataObject != null ? JsonConvert.SerializeObject(dataObject) : "{}";

                using (StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
                {
                    this.ResetRestClientHeaders();
                    var response = await this.httpClient.PutAsync(uri, stringContent).ConfigureAwait(true);
                    response.EnsureSuccessStatusCode();

                    if (!response.IsSuccessStatusCode)
                    {
                        return await Task.FromResult(new TReturnMessage()).ConfigureAwait(true);
                    }

                    var result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                    return JsonConvert.DeserializeObject<TReturnMessage>(result);
                }
            }
         */
    }
}
