using test_things.Services.Actions;
using test_things.Services.Factories;

namespace test_things.Setup;

public static class ServicesRegistration
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<UsersService>()
            .AddScoped<PetsService>()
            .AddScoped<PetsTypesService>()
            .AddScoped<CitiesService>();

        builder.Services
            .AddScoped<UsersFactory>()
            .AddScoped<PetsFactory>()
            .AddScoped<PetsTypesFactory>()
            .AddScoped<CitiesFactory>();

        return builder;
    }
}
