#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim AS build
ARG FEED_URL
ARG PAT 

# download and install latest credential provider. 
# there is a note on the credentialprovider that this is not needed as was to be included on all SDK but it was not implemented
# https://github.com/dotnet/dotnet-docker/issues/878#issuecomment-513978346
RUN wget -qO- https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh | bash


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CSF.SRDashboard.Client/CSF.SRDashboard.Client.csproj", "CSF.SRDashboard.Client/"]
COPY ["MPDIS.API.Wrapper/MPDIS.API.Wrapper.csproj", "MPDIS.API.Wrapper/"]
COPY ["CSF.Common.Library/CSF.Common.Library.csproj", "CSF.Common.Library/"]
COPY ["CSF.Web/CSF.Web.Client/CSF.Web.Client.csproj", "CSF.Web/CSF.Web.Client/"]
COPY ["CSF.Components/CSF.Components.csproj", "CSF.Components/"]
COPY ["CSF.API/CSF.API/CSF.API.csproj", "CSF.API/CSF.API/"]
COPY ["nuget.config", "CSF.SRDashboard.Client/"]

# Environment variable to enable seesion token cache. More on this here: https://github.com/Microsoft/artifacts-credprovider#help
ENV NUGET_CREDENTIALPROVIDER_SESSIONTOKENCACHE_ENABLED true
# Environment variable for adding endpoint credentials. More on this here: https://github.com/Microsoft/artifacts-credprovider#help
# Add "FEED_URL" AND "PAT" using --build-arg in docker build step. "endpointCredentials" field is an array, you can add multiple endpoint configurations.
# Make sure that you *do not* hard code the "PAT" here. That is a sensitive information and must not be checked in.
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS {\"endpointCredentials\": [{\"endpoint\":\"${FEED_URL}\", \"username\":\"ArtifactsDocker\", \"password\":\"${PAT}\"}]}

RUN dotnet restore "CSF.SRDashboard.Client/CSF.SRDashboard.Client.csproj"
COPY . .
WORKDIR "/src/CSF.SRDashboard.Client"
RUN dotnet build "CSF.SRDashboard.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSF.SRDashboard.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSF.SRDashboard.Client.dll"]