#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Request/Request.Api/Request.Api.csproj", "Services/Request/Request.Api/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus.Messages/"]
RUN dotnet restore "Services/Request/Request.Api/Request.Api.csproj"
COPY . .
WORKDIR "/src/Services/Request/Request.Api"
RUN dotnet build "Request.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Request.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Request.Api.dll"]