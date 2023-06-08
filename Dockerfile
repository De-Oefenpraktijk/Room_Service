# Base stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Room_Service/Room_Service.csproj", "Room_Service/"]
RUN dotnet restore "Room_Service/Room_Service.csproj"
COPY . .
WORKDIR "/src/Room_Service"
RUN dotnet build "Room_Service.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Room_Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Development stage
FROM base AS stage
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Room_Service.dll"]

# Production stage
FROM base AS prod
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Room_Service.dll"]
