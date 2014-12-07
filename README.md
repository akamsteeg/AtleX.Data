AtleX.Data
==========

A small utility library to manage Entity Framework contexts.

## Usage

### Initialization

Extend the DbContextReposity class to insert your DbContext object:

```csharp
public class DummyRepository : DbContextRepository<DummyContext>
{
	public DummyRepository()
		: base(new DummyContext())
	{

	}
}
```

### Query data

Query data is done via de `Query<T>()` method which needs an entity type tracked by the context. 

```csharp
using (DbContextRepository repos = new DummyRepository())
{
	// All objects
	var allObjects = repos.Query<DummyObject>().ToList();
}
```

The `IQueryable` returned by `Query<T>()` can be used to customise the query.

```csharp
using (DbContextRepository repos = new DummyRepository())
{
	// All objects created in the last 7 dages
	var allObjects = repos.Query<DummyObject>().Where(o => o.Created >= DateTime.UtcNow.AddDays(-7)).ToList();
}
```

### Adding an object

Just use `Add<T(T objectToAdd)` to add new object.

```csharp
using (DbContextRepository repos = new DummyRepository())
{
	DummyObject dummy = new DummyObject();
	repos.Add<DummyObject>(dummy);
	repos.SaveChanges(); // Don't forget to call this
}
```

### Deleting an object

Call `Delete<T(T objectToDelete)` to delete a state tracked object.

```csharp
using (DbContextRepository repos = new DummyRepository())
{
	DummyObject dummy = repos.Query<DummyObject>().SingleOrDefault(o => o.Id == 1);
	repos.Delete<DummyObject>(dummy);
	repos.SaveChanges(); // Don't forget to call this
}
```

## IHasCreated & IHasLastModified

New entities that implement the `IHasCreated` have the value of their `Created` property set
when `SaveChanges()` is called. Of course it doesn't update this value on modified, as opposed 
to created, entities.

`IHasLastModified` is updated every time an entity is saved, but only if it has any of its values
changed.

## System requirements

AtleX.Data is written in C# and targets .NET 4.5 and Entity Framework 6. The solution file is a
VS2013 one.

## License

AtleX.Data is released under the MIT license, as described in LICENSE.txt.