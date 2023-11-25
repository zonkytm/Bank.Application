FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Bank.Application.Api.Domain/Bank.Application.Api.Domain.csproj", "Bank.Application.Api.Domain/"]
RUN dotnet restore "Bank.Application.Api.Domain/Bank.Application.Api.Domain.csproj"
COPY . .
WORKDIR "/src/Bank.Application.Api.Domain"
RUN dotnet build "Bank.Application.Api.Domain.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bank.Application.Api.Domain.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bank.Application.Api.Domain.dll"]
