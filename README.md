AtleX.Data
==========

A small utility library to extend Entity Framework contexts.

## Installation

```
install-package AtleX.Data -Pre
```

The package is hosted [on NuGet.org](https://www.nuget.org/packages/AtleX.Data/)

## Usage

Extend your context class with `DbContextBase` instead of `DbContext`:

```csharp
public class DummyContext : DbContextBase
{
	public DummyContext()
		: base()
	{

	}
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