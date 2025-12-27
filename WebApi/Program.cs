using Application;
using Application.Services.AnnotationInformation;
using Application.Services.ContentTableSearch;
using Application.Services.EncyclopediaSearch;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .Enrich.WithProperty("ApplicationName", "Scriptorium")
        .WriteTo.Console()
        .WriteTo.DurableHttpUsingFileSizeRolledBuffers(
            requestUri: "http://localhost://",
            bufferBaseFileName: "LogsBuffer",
            textFormatter: new Serilog.Formatting.Json.JsonFormatter()
        );
});

const string corsPolicyName = "AllowAngularApp";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, policy =>
    {
        policy.WithOrigins("http://localhost:")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEncyclopediaSearchService, EncyclopediaSearchService>();
builder.Services.AddScoped<IContentTableService, ContentTableService>();
builder.Services.AddScoped<IAnnotationInformationService, AnnotationInformationService>();

builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddControllers();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors(corsPolicyName);

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

await Log.CloseAndFlushAsync();