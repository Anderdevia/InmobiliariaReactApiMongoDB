using WebApiMongoDB.Data; 
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Añadir Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar MongoDbService y otros servicios que estés utilizando
builder.Services.AddSingleton<PropertyImageService>(); 
builder.Services.AddSingleton<PropertyTraceService>(); 
builder.Services.AddSingleton<PropertyService>(); 
builder.Services.AddSingleton<OwnerService>(); 

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NuevaPolitica");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
