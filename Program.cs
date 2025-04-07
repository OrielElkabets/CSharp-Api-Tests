using Microsoft.EntityFrameworkCore;
using test_things;
using test_things.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TestDbContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("DemoDB");
    if (connection is null) throw new Exception("Couldn't find TestDb connection string");
    options.UseNpgsql(connection).UseSnakeCaseNamingConvention();
});

builder.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();