FROM mcr.microsoft.com/dotnet/sdk:8.0 AS development

WORKDIR /usr/src/app

COPY . .

COPY UserApi.csproj .

RUN dotnet restore UserApi.csproj

COPY . .

CMD ["dotnet", "run"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-production-files

WORKDIR /usr/src/app

COPY UserApi.csproj .

RUN dotnet restore UserApi.csproj

COPY . .

RUN mkdir -p /usr/src/app/publish
RUN dotnet publish UserApi.csproj -c Release -o /usr/src/app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS production

WORKDIR /usr/src/app

COPY --from=build-production-files /usr/src/app/publish .

CMD dotnet UserApi.dll