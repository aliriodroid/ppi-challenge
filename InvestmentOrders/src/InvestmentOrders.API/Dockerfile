FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/InvestmentOrders.API/InvestmentOrders.API.csproj", "src/InvestmentOrders.API/"]
COPY ["src/InvestmentOrders.Application/InvestmentOrders.Application.csproj", "src/InvestmentOrders.Application/"]
COPY ["src/InvestmentOrders.Domain/InvestmentOrders.Domain.csproj", "src/InvestmentOrders.Domain/"]
COPY ["src/InvestmentOrders.Infrastructure/InvestmentOrders.Infrastructure.csproj", "src/InvestmentOrders.Infrastructure/"]
RUN dotnet restore "src/InvestmentOrders.API/InvestmentOrders.API.csproj"
COPY . .
WORKDIR "/src/src/InvestmentOrders.API"
RUN dotnet build "InvestmentOrders.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InvestmentOrders.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvestmentOrders.API.dll"]