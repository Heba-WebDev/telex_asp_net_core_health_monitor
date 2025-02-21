using API.Contracts;
using API.Extensions;
using API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureCors();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IWebhookService, WebhookService>();

builder.Services.AddHealthChecks()
    .AddCheck<SystemHealthCheck>("system_health_check");

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowTelex");
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Map health checks
app.MapHealthChecks("/health");

app.Run();