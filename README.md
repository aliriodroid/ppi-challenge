# Investment Orders API

API REST desarrollada en .NET 8 para la gestión de órdenes de inversión en el mercado financiero. Permite crear, consultar, actualizar y eliminar órdenes de inversión para diferentes tipos de activos (Acciones, Bonos y FCIs).

## Tecnologías Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Docker
- Swagger
- NUnit para pruebas unitarias
- FluentValidation
- AutoMapper

## Arquitectura

El proyecto sigue los principios de Clean Architecture y está organizado en las siguientes capas:

- **Domain**: Entidades y reglas de negocio
- **Application**: Casos de uso y DTOs
- **Infrastructure**: Implementaciones de persistencia
- **API**: Controllers y configuración de la aplicación
- **Tests**: Pruebas unitarias y de integración

## Requisitos Previos

Para ejecutar localmente:
- .NET SDK 8.0
- PostgreSQL
- Visual Studio 2022, VS Code o Rider

Para ejecutar con Docker:
- Docker Desktop
- Docker Compose

## Ejecución Local

1. Clonar el repositorio:
```bash
git clone https://github.com/aliriodroid/ppi-challenge.git
cd InvestmentOrders
```

2. Restaurar dependencias:
```bash
dotnet restore
```

3. Configurar la cadena de conexión en `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=investment_orders;Username=ppi;Password=ppi123;Port=5432"
  }
}
```

4. Levantar la base de datos:
```bash
docker-compose up db
```

5. Aplicar las migraciones:
```bash
cd src/InvestmentOrders.Infrastructure
dotnet ef database update
```

6. Ejecutar la aplicación:
```bash
cd ../InvestmentOrders.API
dotnet run
```

La API estará disponible en:
- https://localhost:7241/swagger/index.html (HTTPS)
- http://localhost:5241/swagger/index.html (HTTP)

## Ejecución con Docker

1. Construir y ejecutar los contenedores:
```bash
docker-compose up --build
```

2. La API estará disponible en:
```
http://localhost:8080/swagger/index.html
```

## Pruebas de la API

Ejemplos de peticiones para probar la API:

1. Crear orden de Acción (No requiere precio):
```json
{
  "idCuenta": 1,
  "nombreActivo": "Apple",
  "cantidad": 100,
  "operacion": "C",
  "activoId": 1
}
```

2. Crear orden de Bono (Requiere precio):
```json
{
  "idCuenta": 1,
  "nombreActivo": "BONOS ARGENTINA USD 2030",
  "cantidad": 1000,
  "precio": 307.40,
  "operacion": "C",
  "activoId": 6
}
```

3. Crear orden de FCI (Requiere precio):
```json
{
  "idCuenta": 1,
  "nombreActivo": "Delta Pesos Clase A",
  "cantidad": 5000,
  "precio": 0.0181,
  "operacion": "C",
  "activoId": 8
}
```

## Activos Disponibles

### Acciones (TipoActivoId: 1)
- AAPL (Id: 1) - Apple - $177.97
- GOOGL (Id: 2) - Alphabet Inc - $138.21
- MSFT (Id: 3) - Microsoft - $329.04
- KO (Id: 4) - Coca Cola - $58.30
- WMT (Id: 5) - Walmart - $163.42

### Bonos (TipoActivoId: 2)
- AL30 (Id: 6) - BONOS ARGENTINA USD 2030 L.A - $307.40
- GD30 (Id: 7) - Bonos Globales Argentina USD Step Up 2030 - $336.10

### FCIs (TipoActivoId: 3)
- Delta.Pesos (Id: 8) - Delta Pesos Clase A - $0.0181
- Fima.Premium (Id: 9) - Fima Premium Clase A - $0.0317

## Ejecutar Tests

```bash
cd tests/InvestmentOrders.UnitTests
dotnet test
```

## Notas Adicionales

- Las acciones utilizan el precio de la base de datos, no requieren precio en la petición
- Los bonos y FCIs requieren precio en la petición
- Las comisiones se calculan automáticamente según el tipo de activo:
  - Acciones: 0.6% + 21% de impuesto sobre la comisión
  - Bonos: 0.2% + 21% de impuesto sobre la comisión
  - FCIs: Sin comisiones ni impuestos
