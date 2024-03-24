FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Use the .NET SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file and restore any dependencies
COPY ["KeyNekretnine.Api/KeyNekretnine.Api.csproj", "KeyNekretnine.Api/"]
COPY ["KeyNekretnine.Application/KeyNekretnine.Application.csproj", "KeyNekretnine.Application/"]
COPY ["KeyNekretnine.Domain/KeyNekretnine.Domain.csproj", "KeyNekretnine.Domain/"]
COPY ["KeyNekretnine.Infrastructure/KeyNekretnine.Infrastructure.csproj", "KeyNekretnine.Infrastructure/"]
RUN dotnet restore "KeyNekretnine.Api/KeyNekretnine.Api.csproj"

# Copy the project files
COPY . .

# Build the project
WORKDIR "/src/KeyNekretnine.Api"
RUN dotnet build "KeyNekretnine.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "KeyNekretnine.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Generate the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "KeyNekretnine.Api.dll"]