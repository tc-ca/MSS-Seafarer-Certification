#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CSF.Web.Client.csproj", "CSF.Web.Client/"]
RUN dotnet restore "CSF.Web.Client/CSF.Web.Client.csproj"
ARG target="CSF.Web.Client/"
COPY . ${target}
WORKDIR "/src/CSF.Web.Client"
RUN dotnet build "CSF.Web.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSF.Web.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSF.Web.Client.dll"]