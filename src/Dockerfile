# Use the ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

# Use the .NET SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the CSPROJ files and restore any dependencies
COPY ["src/KeyNekretnine.Api/KeyNekretnine2.Api.csproj", "src/KeyNekretnine.Api/"]
COPY ["src/KeyNekretnine.Application/KeyNekretnine.Application.csproj", "src/KeyNekretnine.Application/"]
COPY ["src/KeyNekretnine.Domain/KeyNekretnine.Domain.csproj", "src/KeyNekretnine.Domain/"]
COPY ["src/KeyNekretnine.Infrastructure/KeyNekretnine.Infrastructure.csproj", "src/KeyNekretnine.Infrastructure/"]
RUN dotnet restore "src/KeyNekretnine.Api/KeyNekretnine.Api.csproj"

# Copy the project files
COPY src/ src/

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
