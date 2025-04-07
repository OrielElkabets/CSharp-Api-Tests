using test_things.Exceptions;

namespace test_things.DTOs;

public static class MappingHelper
{
    public static TTo? MapOrNull<TFrom, TTo>(TFrom? entity, Func<TFrom, TTo> mapper)
        where TTo : class
        where TFrom : class
    {
        if (entity is null) return null;
        return mapper(entity);
    }

    public static TTo MapOrThrow<TFrom, TTo>(TFrom? entity, Func<TFrom, TTo> mapper)
        where TTo : class
        where TFrom : class
    {
        if (entity is null) throw new EntityNotLoadedException(typeof(TFrom).Name);
        return mapper(entity);
    }
}