# MovieManager

Gestione di film, attori, registi e recensioni con architettura a più livelli (.NET 10).

## Architettura

```
MovieManager.slnx
 ├── MovieManager.DAL          – Data Access Layer (EF Core, SQL Server)
 ├── MovieManager.BLL          – Business Logic Layer (AutoMapper, servizi generici)
 ├── MovieManager.PL.API       – REST API (Scalar/OpenAPI)
 └── MovieManager.PL.MVC       – Web App MVC (Bootstrap, jQuery)
```

### DAL (Data Access Layer)
- Entity Framework Core 10.0.9 con provider SQL Server
- Pattern **Repository** generico (`GenericRepository<T>`) + **Unit of Work**
- Repository dedicato `MovieActorRepository` per la gestione della chiave composta
- DbContext con configurazione Fluent API e vincoli di integrità (es. `Review.Score` tra 1 e 10)

### BLL (Business Logic Layer)
- Servizio generico `GenericService<TEntity, TModel>` con vincolo `IModelWithId`
- `MovieActorService` dedicato per entità a chiave composta (`MovieId`, `ActorId`)
- Mapping Entity ↔ Model con **AutoMapper 14** (6 profili bidirezionali)

### PL.API (REST API)
- 30 endpoint RESTful su 6 controller (Actors, Directors, Genres, Movies, Reviews, MovieActors)
- Documentazione OpenAPI tramite **Scalar** (`/scalar/v1`)
- Risposte con messaggi descrittivi in inglese (es. `"Actor with id {id} not found."`)

### PL.MVC (Web App)
- Interfaccia utente con ASP.NET Core MVC
- Bootstrap 5, jQuery, jQuery Validation
- **Stato attuale**: scaffold di default, non ancora collegato ai servizi BLL/DAL

## Modello dei Dati

```
Director ──< Movie >── Genre
                │
                ├──< MovieActor >── Actor
                │
                └──< Review
```

| Entità | Campi principali |
|---|---|
| `Movie` | Id, Title, Description, ReleaseDate, DurationMinutes, Language, Country, Budget, BoxOffice, Rating, ImageUrl, GenreId, DirectorId |
| `Actor` | Id, FirstName, LastName, BirthDate?, Country?, Biography? |
| `Director` | Id, FirstName, LastName, BirthDate?, Country?, Biography? |
| `Genre` | Id, Name, Description? |
| `Review` | Id, MovieId, ReviewerName, Score (1-10), Comment?, CreatedAt |
| `MovieActor` | MovieId + ActorId (chiave composta), CharacterName, IsLeadRole, DisplayOrder |

## Tecnologie

| Componente | Versione |
|---|---|
| .NET | 10.0 |
| Entity Framework Core | 10.0.9 |
| Entity Framework Core SqlServer | 10.0.9 |
| AutoMapper | 14.0.0 |
| Scalar.AspNetCore | 2.16.11 |
| Microsoft.AspNetCore.OpenApi | 10.0.9 |
| SQL Server (LocalDB) | — |
| Bootstrap | 5 |

## Endpoint API

### Actors

| Metodo | Route | Descrizione |
|---|---|---|
| GET | `/api/actors` | Elenco attori |
| GET | `/api/actors/{id}` | Attore per id |
| POST | `/api/actors` | Crea attore |
| PUT | `/api/actors/{id}` | Aggiorna attore |
| DELETE | `/api/actors/{id}` | Elimina attore |

### Directors

| Metodo | Route | Descrizione |
|---|---|---|
| GET | `/api/directors` | Elenco registi |
| GET | `/api/directors/{id}` | Regista per id |
| POST | `/api/directors` | Crea regista |
| PUT | `/api/directors/{id}` | Aggiorna regista |
| DELETE | `/api/directors/{id}` | Elimina regista |

### Genres

| Metodo | Route | Descrizione |
|---|---|---|
| GET | `/api/genres` | Elenco generi |
| GET | `/api/genres/{id}` | Genere per id |
| POST | `/api/genres` | Crea genere |
| PUT | `/api/genres/{id}` | Aggiorna genere |
| DELETE | `/api/genres/{id}` | Elimina genere |

### Movies

| Metodo | Route | Descrizione |
|---|---|---|
| GET | `/api/movies` | Elenco film |
| GET | `/api/movies/{id}` | Film per id |
| POST | `/api/movies` | Crea film |
| PUT | `/api/movies/{id}` | Aggiorna film |
| DELETE | `/api/movies/{id}` | Elimina film |

### Reviews

| Metodo | Route | Descrizione |
|---|---|---|
| GET | `/api/reviews` | Elenco recensioni |
| GET | `/api/reviews/{id}` | Recensione per id |
| POST | `/api/reviews` | Crea recensione |
| PUT | `/api/reviews/{id}` | Aggiorna recensione |
| DELETE | `/api/reviews/{id}` | Elimina recensione |

### MovieActors

| Metodo | Route | Descrizione |
|---|---|---|
| GET | `/api/movieactors/movie/{movieId}` | Ruoli per film |
| GET | `/api/movieactors/{movieId}/{actorId}` | Ruolo specifico |
| POST | `/api/movieactors` | Crea associazione |
| PUT | `/api/movieactors/{movieId}/{actorId}` | Aggiorna associazione |
| DELETE | `/api/movieactors/{movieId}/{actorId}` | Elimina associazione |

## Prerequisiti

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB, locale o remoto)

## Configurazione

La stringa di connessione predefinita in `MovieManager.PL.API/appsettings.json` utilizza LocalDB:

```json
{
  "ConnectionStrings": {
    "MovieDBString": "Server=(localdb)\\MSSQLLocalDB;Database=MovieManager;Trusted_Connection=True;"
  }
}
```

Per un SQL Server diverso, modificare la stringa di connessione di conseguenza.

## Migrazioni del Database

```bash
# Applicare le migrazioni
dotnet ef database update --project MovieManager.DAL --startup-project MovieManager.PL.API
```

## Esecuzione

```bash
# API REST (https://localhost:7086, documentazione Scalar su /scalar/v1)
dotnet run --project MovieManager.PL.API

# MVC (https://localhost:7274)
dotnet run --project MovieManager.PL.MVC
```

## Note sullo Stato del Progetto

- **DAL / BLL / PL.API**: completi e funzionanti
- **PL.MVC**: scaffold di default, i servizi BLL/DAL non sono ancora configurati. Da implementare: registrazione DI, controller, viste e view model per la gestione dei film
- **Test**: nessun progetto di test presente
- **CI/CD**: nessuna pipeline configurata (`.github/workflows/` vuoto)

## Licenza

MIT © 2026 Davide Barbieri
