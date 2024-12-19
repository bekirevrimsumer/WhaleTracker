using Microsoft.EntityFrameworkCore;
using WhaleTracker.Infrastructure.Data;
using WhaleTracker.Core.Interfaces;
using WhaleTracker.Infrastructure.Services;
using WhaleTracker.Infrastructure.Repositories;
using WhaleTracker.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder
                .SetIsOriginAllowed(_ => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

// Database Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository Registration
builder.Services.AddScoped<IWalletRepository, WalletRepository>();

// SignalR
builder.Services.AddSignalR();

// Notification Service
builder.Services.AddTransient<INotificationService, SignalRNotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// SignalR endpoint'i
app.MapHub<WalletHub>("/hubs/wallet");

app.Run(); 