using Microsoft.AspNetCore.Http.HttpResults;

namespace test_things.Errors;

public class ErrorBase(string message)
{
    public string Message { get; } = message;
}

public class NotFoundError(string message) : ErrorBase(message)
{
    public int Code { get; } = StatusCodes.Status404NotFound;
}

public class PropertyNotFoundError(
    string type,
    string property,
    object value) : NotFoundError($"{type} with {property} '{value}' not found")
{ }

public class BadRequestError(string message) : ErrorBase(message)
{
    public int Code { get; } = StatusCodes.Status400BadRequest;
}

public class ExistError(
    string type,
    string property,
    object value) :
    BadRequestError($"{type} with {property} '{value}' allready exist")
{ }