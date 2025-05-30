# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Server/WebAPI/WebAPI.csproj", "Server/WebAPI/"]
COPY ["Server/Application/Application.csproj", "Server/Application/"]
COPY ["Server/Domain/Domain.csproj", "Server/Domain/"]
COPY ["Server/Infrastructure/Infrastructure.csproj", "Server/Infrastructure/"]
RUN dotnet restore "./Server/WebAPI/WebAPI.csproj"
COPY . .
WORKDIR "/src/Server/WebAPI"
RUN dotnet build "./WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]