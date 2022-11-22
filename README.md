# URL Shortener
![alt text for screen readers](/images/diagram.png "Diagram")
## About <a name = "about"></a>

This a url shortener built with minimal API, Mongodb and [Angular]("https://github.com/MarcosFerreira17/url-shortener-spa").

### Prerequisites

What things you need to install the software and how to install them.

```
- .NET 6
- Angular 14.2.4. (Just in case you use the complete project with frontend)
- Docker - Docker compose (optional, only if you must run entire application.)
- Mongodb
```

### Installing

How to run the API:

```
dotnet watch run
```
#####   To run only the api you must configure mongodb first
How to run the entire project:
```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up
```
For the API to work correctly the database must already be running.
## Minimal API .NET 6
The new .NET 6.0 feature - Minimal APIs - for ASP .NET Core 6 allows you to create APIs with minimal dependency on the WebAPI framework and minimal code and files required for minimalist API development.

Now ASP .NET Core follows other technologies like GO, Python and Node.js that already allowed the creation of APIs with minimal code and dependencies.

As main objectives, the Minimals APIs of the .NET platform allow:

- Reduce code complexity for those just starting out;

- Reduce non-essential features (controllers, routing, filters, etc) that were present in ASP.NET projects;

- Embrace the concept of API-level minimalism;
Be Scalable, ensuring that your application can grow if necessary;

## MongoDB
MongoDB is a source-available cross-platform document-oriented database program. Classified as a NoSQL database program, MongoDB uses JSON-like documents with optional schemas. MongoDB is developed by MongoDB Inc. and licensed under the Server Side Public License which is deemed non-free by several distributions.

![alt text for screen readers](/images/linky.PNG "Angular spa")