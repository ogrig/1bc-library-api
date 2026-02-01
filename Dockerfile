# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project file and restore
COPY ["LibraryAPI.csproj", "."]
RUN dotnet restore "LibraryAPI.csproj"

# Copy source and publish
COPY . .
RUN dotnet publish "LibraryAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 5250

# Match launchSettings / LibraryAPI.http so the same port works for local and Docker
ENV ASPNETCORE_URLS=http://+:5250

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LibraryAPI.dll"]
