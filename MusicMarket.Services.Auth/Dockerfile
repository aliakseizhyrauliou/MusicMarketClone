FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /source

COPY . .

RUN dotnet restore "./MusicMarket.Services.Auth.csproj" --disable-parallel

RUN dotnet publish "./MusicMarket.Services.Auth.csproj" -c release -o /app --no-restore



FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY --from=build /app ./

EXPOSE 5000   

ENTRYPOINT ["dotnet", "MusicMarket.Services.Auth.dll"]


