# Premise

A collection of base libraries for .NET Core applications providing quick scaffolding of common patterns — generic repositories, Entity Framework integration, and ASP.NET MVC helpers.

## Packages

| Package | Description |
|---------|-------------|
| `Premise.Data` | Core interfaces for the generic repository pattern |
| `Premise.Data.EntityFramework` | Entity Framework implementation of `IRepository` |
| `Premise.Web` | ASP.NET Core MVC helpers (select list extensions, etc.) |

## Premise.Data

Provides `IRepository<K, T>` — a generic repository interface for CRUD operations against any data store. Program against the interface so the underlying provider can be swapped with minimal changes to business logic.

**Full interface:** `Get`, `GetAll`, `Insert`, `InsertMany`, `Update`, `UpdateMany`, `Delete`, `DeleteMany`, `Commit` — all with sync and async variants. Also includes `Reload` for refreshing entities from the store.

Entities implement `IEntity<K>`, or extend `BaseEntity<K>` for a ready-made `Id` property.

## Premise.Data.EntityFramework

Concrete `EntityRepository<K, T>` implementation backed by Entity Framework Core. Supports automatic GUID generation for new entities.

```csharp
// Define a repository
public class BookRepository : EntityRepository<Guid, Book>
{
    public BookRepository(DbContext context) : base(context) { }
}

// Use it
var repo = new BookRepository(dbContext);

// Sync
var books = repo.GetAll();
repo.Insert(new Book { Title = "Example" });
repo.Commit();

// Async
var books = await repo.GetAllAsync();
await repo.InsertAsync(new Book { Title = "Example" });
await repo.CommitAsync();

// Bulk operations
await repo.InsertManyAsync(bookList);
await repo.DeleteManyAsync(new[] { id1, id2 });
```

## Premise.Web

ASP.NET Core MVC helpers.

**`SelectListExtensions.FromEnum<T>()`** — builds a `SelectListItem` collection from any enum for use in Razor dropdowns:

```csharp
public enum Genre { Fiction, NonFiction, Mystery }

// In your controller/view model
ViewBag.Genres = SelectListExtensions.FromEnum<Genre>();
```

```html
@Html.DropDownListFor(m => m.Genre, (IEnumerable<SelectListItem>)ViewBag.Genres)
```

## Building

```bash
cd src
bash build.sh
```

## License

MIT License. See [LICENSE](LICENSE) for details.
