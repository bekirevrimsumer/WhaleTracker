using Microsoft.EntityFrameworkCore;
using WhaleTracker.Infrastructure.Data;
using WhaleTracker.Core.Interfaces;
using WhaleTracker.Infrastructure.Services;
using WhaleTracker.Infrastructure.Repositories;
using WhaleTracker.API.Hubs;
using WhaleTracker.Infrastructure.BackgroundServices;

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
                .WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials(); // SignalR için gerekli
        });
});

// Database Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Solscan Service Registration
builder.Services.AddHttpClient<ISolscanService, SolscanService>();

builder.Services.AddHostedService<WalletMonitoringService>();

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

// CORS middleware'ini UseRouting'den önce ekleyin
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// SignalR endpoint'i
app.MapHub<WalletHub>("/hubs/wallet");

app.Run(); 