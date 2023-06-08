# Base stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Profile_Service/Profile_Service.csproj", "Profile_Service/"]
RUN dotnet restore "Profile_Service/Profile_Service.csproj"
COPY . .
WORKDIR "/src/Profile_Service"
RUN dotnet build "Profile_Service.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Profile_Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Development stage
FROM base AS stage
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Profile_Service.dll"]

# Production stage
FROM base AS prod
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Profile_Service.dll"]
