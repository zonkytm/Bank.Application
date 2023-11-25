FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Bank.Application.Migrations/Bank.Application.Migrations.csproj", "Bank.Application.Migrations/"]
RUN dotnet restore "Bank.Application.Migrations/Bank.Application.Migrations.csproj"
COPY . .
WORKDIR "/src/Bank.Application.Migrations"
RUN dotnet build "Bank.Application.Migrations.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bank.Application.Migrations.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bank.Application.Migrations.dll"]
