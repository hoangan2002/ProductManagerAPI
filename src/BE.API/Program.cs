using BE.API.Middleware;
using BE.Application.DependencyInjection.Extentions;
using BE.Persistance.DependencyInjection.Extensions;
using BE.Persistance.DependencyInjection.Options;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// Add Log.
Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();
builder.Logging
    .ClearProviders()
    .AddSerilog();

builder.Host.UseSerilog();
// Add configuration
builder.Services.AddConfigureMediatR();
builder.Services.ConfigureSqlServerRetryOptions(builder.Configuration.GetSection(nameof(SqlServerRetryOptions)));
builder.Services.AddSqlConfiguration();
builder.Services.AddRepositoryBaseConfiguration();
builder.Services.AddConfigureAutoMapper();
// Api
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
