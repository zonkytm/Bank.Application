FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Bank.Application.Host/Bank.Application.Host.csproj", "Bank.Application.Host/"]
RUN dotnet restore "Bank.Application.Host/Bank.Application.Host.csproj"
COPY . .
WORKDIR "/src/Bank.Application.Host"
RUN dotnet build "Bank.Application.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bank.Application.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bank.Application.Host.dll"]
