FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/OrbitTracker.Api/OrbitTracker.Api.csproj", "src/OrbitTracker.Api/"]
COPY ["src/OrbitTracker.Core/OrbitTracker.Core.csproj", "src/OrbitTracker.Core/"]
COPY ["src/OrbitTracker.Infrastructure/OrbitTracker.Infrastructure.csproj", "src/OrbitTracker.Infrastructure/"]
RUN dotnet restore "src/OrbitTracker.Api/OrbitTracker.Api.csproj"
COPY . .
WORKDIR "/src/src/OrbitTracker.Api"
RUN dotnet build "OrbitTracker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OrbitTracker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrbitTracker.Api.dll"]
