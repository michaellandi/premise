#Premise
A collection of libraries for .net core applications to support quick and easy scaffolding of applications :).

This library is intended to help .net developers quickly scaffold and build great applications!  From providing support for generic repositories and advanced data structures to helpful UI helpers for popular frameworks, Premise will help save development time and redundant plumbing code in every application!

----

## Premise.Data
The *Premise.Data* namespace provides all of the neccessary interfaces to implement a generic repository pattern.  The main deliverable of this package is the `IRepository<K,T>` interface which is used to represent a generic repository (with no particular implementation).  This repository can be used by your application to provide an interface to code against.  While using the interface, the underlying data store can be swapped with minimal changes to your business logic.

Initializing an `Repository<K,T>` will quickly provide basic CRUD functionality.  Dependency injection is supported through the use of the generic IRepository interface, allowing underlying data providers to be swapped at runtime.

## Premise.Data.EntityFramework

The *Premise.Data.EntityFramework* namespace provides a concrete implementation of the `IRepository` interface provided in the `Premise.Data` package.  The implementation is based upon using Entity Framework as the underlying data store.

```
//Declare new book repository with a Guid Primary Key
public class BookRepository : Repository<Guid, Book>()
{
	public BookRepository(IDataContext context) 
	{
		//...
	}
}
```

```
//CRUD actions available for use
var repository = new BookRepository(DbContext);
var books = repository.GetAll();
```

```
//Async/Await Api support
var repository = new BookRepository(DbContext);
var books = await repository.GetAllAsync();
```

Support for advanced paging is automatically brought in using the generic *Pageable* interface.
```
[HttpGet]
public async Task<IActionResult> IndexAsync(int page, int pageSize = 10)
{
	var repository = new Repository<Book>();
	var books = await repository.GetAllAsync(page, pageSize);

    return View(books);
}
```

