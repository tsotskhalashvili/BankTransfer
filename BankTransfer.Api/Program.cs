using BankTransfer.Application.Extensions;
using BankTransfer.Persistence.Context;
using BankTransfer.Persistence.Extensions;
using BankTransfer.Persistence.Seed;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

const string DevCorsPolicy = "AllowReactDev";

var allowedOrigin = builder.Configuration["Cors:AllowedOrigin"] ?? "http://localhost:5173";

builder.Services.AddCors(options =>
{
    options.AddPolicy(DevCorsPolicy, policy =>
        policy.WithOrigins(allowedOrigin)
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BankTransferDbContext>();
    await db.Database.MigrateAsync();
    await DataSeeder.SeedAsync(db);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<BankTransfer.Api.Middleware.ExceptionMiddleware>();

app.UseCors(DevCorsPolicy);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();