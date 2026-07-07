# MovieManager

Gestione di film, attori, registi e recensioni con architettura a più livelli (.NET 10).

## Architettura

```
MovieManager.slnx
 ├── MovieManager.DAL          – Data Access Layer (EF Core, SQL Server)
 ├── MovieManager.BLL          – Business Logic Layer (AutoMapper, servizi generici)
 ├── MovieManager.PL.API       – REST API (Swagger/OpenAPI)
 └── MovieManager.PL.MVC       – Web App MVC (Bootstrap, jQuery)
```

### DAL
- Entity Framework Core 10.0.9 con SQL Server
- Pattern **Repository** generico + **Unit of Work**
- Repository dedicato `MovieActorRepository` per chiave composta

### BLL
- Servizio generico `GenericService<TEntity, TModel>` con vincolo `IModelWithId`
- `MovieActorService` dedicato per entità a chiave composta
- Mapping Entity ↔ Model con **AutoMapper 14**

### PL.API
- Endpoint RESTful con Swagger UI
- Dipende da BLL per la logica applicativa

### PL.MVC
- Interfaccia utente con ASP.NET Core MVC
- Bootstrap 5, jQuery, jQuery Validation

## Tecnologie

| Componente | Versione |
|---|---|
| .NET | 10.0 |
| Entity Framework Core | 10.0.9 |
| AutoMapper | 14.0.0 |
| Swashbuckle | 10.2.3 |
| SQL Server | — |

## Prerequisiti

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- SQL Server (locale o remoto)

## Configurazione

Modificare la stringa di connessione in `MovieManager.PL.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "MovieDBString": "Server=localhost;Database=MovieManager;User Id=sa;Password=root"
  }
}
```

## Esecuzione

```bash
# API
dotnet run --project MovieManager.PL.API

# MVC
dotnet run --project MovieManager.PL.MVC
```

## Licenza

MIT © 2026 Davide Barbieri
