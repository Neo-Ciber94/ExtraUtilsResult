# ExtraUtils.Result
Provides an implementation of the functional type Result as
``Result``, ``Result<T>`` and ``Result<T, TError>`` the represent
the result of an operator that can be either a successful value or an error.

## Implementation
Each result type inherit from ``IResult``.

```csharp
public interface IResult
{
    public bool IsSuccess { get; }
    public bool IsError { get; }
}
```

And provides methods for *get*, *check* or *transform* the result and error.

## Usage
Import the namespace
```csharp
using ExtraUtils;
```

A ``Result`` should be created using ``Result.Ok(...)`` for success
or ```Result.Error(...)``` for errors.

```csharp
public static Result<Person> FindById(int id)
{
    if(Database.TryGetValue(out Person person))
    {
        return Result.Ok(person);
    }

    return Result.Error($"Cannot find person with id {id}");
}
```

Then using the ``Result``
```csharp
Result<Person> result = FindById(138102);

if(result.IsSuccess)
{
    // Use value
    Person value = result.Value;
}
```

For convenience there is an implicit convention from
``Result<T>`` and ``Result<T, TError`` to ``T``, although
is recomended use methods like ``TryGetResult`` or ``OnSuccess``
to avoid possible exceptions.

```csharp
int value = Result.Ok(123);
```
If the ``Result`` is an error ```InvalidOperationException``` will be
throw when trying to get the value.

```csharp
// InvalidOperationException
int value = Result.Error<int>("Invalid result").Value;
```

Also there is a down conversion between ``Result``.
```csharp
Result<int, Exception> result1 = Result.Ok<int, Exception>(10);
Result<int> result2 = result1;
Result result3 = result2;
```