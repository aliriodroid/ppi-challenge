FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/InvestmentOrders.API/InvestmentOrders.API.csproj", "src/InvestmentOrders.API/"]
COPY ["src/InvestmentOrders.Application/InvestmentOrders.Application.csproj", "src/InvestmentOrders.Application/"]
COPY ["src/InvestmentOrders.Domain/InvestmentOrders.Domain.csproj", "src/InvestmentOrders.Domain/"]
COPY ["src/InvestmentOrders.Infrastructure/InvestmentOrders.Infrastructure.csproj", "src/InvestmentOrders.Infrastructure/"]
RUN dotnet restore "./src/InvestmentOrders.API/InvestmentOrders.API.csproj"
COPY . .
WORKDIR "/src/src/InvestmentOrders.API"
RUN dotnet build "./InvestmentOrders.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InvestmentOrders.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "InvestmentOrders.API.dll"]