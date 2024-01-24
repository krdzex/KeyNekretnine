

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/KeyNekretnine.Api/KeyNekretnine.Api.csproj", "src/KeyNekretnine.Api/"]
RUN dotnet restore "src/KeyNekretnine.Api/KeyNekretnine.Api.csproj"
COPY . .
WORKDIR "/src/src/KeyNekretnine.Api"
RUN dotnet build "KeyNekretnine.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KeyNekretnine.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "KeyNekretnine.Api.dll"]
