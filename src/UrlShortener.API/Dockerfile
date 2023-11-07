FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URL=http://localhost:8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/UrlShortener.API/UrlShortener.API.csproj", "UrlShortener.API/"]
COPY ["src/UrlShortener.Application/UrlShortener.Application.csproj", "UrlShortener.Application/"]
COPY ["src/UrlShortener.Infra/UrlShortener.Infra.csproj", "UrlShortener.Infra/"]
COPY ["src/UrlShortener.Domain/UrlShortener.Domain.csproj", "UrlShortener.Domain/"]
RUN dotnet restore "UrlShortener.API/UrlShortener.API.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "src/UrlShortener.API/UrlShortener.API.csproj" -c Release -o /app/build

# Estágio de Publicação
FROM build AS publish
RUN dotnet publish "src/UrlShortener.API/UrlShortener.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio Final
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrlShortener.API.dll"]
