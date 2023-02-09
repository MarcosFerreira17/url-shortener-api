FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /app
# Copy csproj and restore as distinct layers
COPY UrlShortenerAPI.csproj ./
COPY UrlShortenerAPI.sln ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out
RUN dotnet dev-certs https --trust

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "UrlShortenerAPI.dll"]