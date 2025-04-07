namespace test_things.Exceptions;

public class EntityNotLoadedException(string entityName)
    : Exception($"Entity {entityName} should be loaded here")
{}