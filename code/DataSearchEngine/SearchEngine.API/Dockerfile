#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 7498

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SearchEngine.API/SearchEngine.API.csproj", "SearchEngine.API/"]
COPY ["SearchEngine.Application/SearchEngine.Application.csproj", "SearchEngine.Application/"]
COPY ["SearchEngine.Domain/SearchEngine.Domain.csproj", "SearchEngine.Domain/"]
COPY ["SearchEngine.Persistence/SearchEngine.Persistence.csproj", "SearchEngine.Persistence/"]
RUN dotnet restore "./SearchEngine.API/./SearchEngine.API.csproj"
COPY . .
WORKDIR "/src/SearchEngine.API"
RUN dotnet build "./SearchEngine.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SearchEngine.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SearchEngine.API.dll"]