#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["url-shortener-api/UrlShortenerAPI.csproj", "UrlShortenerAPI/"]
RUN dotnet restore "url-shortener-api/UrlShortenerAPI.csproj"
COPY . .
WORKDIR "/src/url-shortener-api"
RUN dotnet build "UrlShortenerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UrlShortenerAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrlShortenerAPI.dll"]
