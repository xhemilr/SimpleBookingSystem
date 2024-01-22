
using SimpleBookingSystem.Server.Extensions;
using SimpleBookingSystem.Server.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Create and initialize DB
builder.Services.AddAppDbContext(configuration);
builder.Services.InitializeDb();

// Register repositories
builder.Services.AddRepositories();

// Add Application layers
builder.Services.AddApplicationLayers();

// Add services to the container.
builder.Services.RegisterServices();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors(opt => opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<RequestResponseLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
