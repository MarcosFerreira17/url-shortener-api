FROM mcr.microsoft.com/dotnet/aspnet:6.0 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY . /url-shortener-api
WORKDIR /url-shortener-api
RUN ls
RUN dotnet build "UrlShortenerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UrlShortenerAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrlShortenerAPI.dll"]