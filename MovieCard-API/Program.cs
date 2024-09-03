using Microsoft.EntityFrameworkCore;
using MovieCard_API.Data;
using MovieCard_API.MovieRepository;
using MovieCard_API.SeedData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MovieCardContext>(options => options
    .UseSqlite(builder.Configuration.GetConnectionString("MovieCardContext") ?? string.Empty));
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddScoped<MovieRepository>();
builder.Services.AddSingleton<SeedMovies>();
builder.Services.AddAutoMapper(typeof(MapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<MovieCardContext>();
        var seeder = services.GetRequiredService<SeedMovies>();
        await seeder.InitData(context);
    }
    catch (Exception error)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(error, "An error occurred while seeding the database.");
    }
}

app.UseHttpsRedirection();

app.MapControllers();


app.Run();

