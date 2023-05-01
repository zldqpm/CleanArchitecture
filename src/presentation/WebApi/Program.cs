using Data;
using Application;
using Identity;
using Shared;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApi.Extensions;
using WebApi.Helpers;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Identity.Helper;
using System.Reflection;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Events;
using Serilog.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//AutoMapper��FluentValidation��MediatR
builder.Services.AddApplication(builder.Configuration);
//EF Core
builder.Services.AddInfrastructureData(builder.Configuration);
//Jwt��Shared
builder.Services.AddInfrastructureShared(builder.Configuration);
builder.Services.AddInfrastructureIdentity(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//Extension
//swagger
builder.Services.AddApiVersioningExtension();
builder.Services.AddVersionedApiExplorerExtension();
builder.Services.AddSwaggerGenExtension();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddCors(c =>
{

    //һ��������ַ���
    c.AddPolicy("LimitRequests", policy =>
    {
        // ֧�ֶ�������˿ڣ�ע��˿ںź�Ҫ��/б�ˣ�����localhost:8000/���Ǵ��
        // ע�⣬http://127.0.0.1:5401 �� http://localhost:5401 �ǲ�һ���ģ�����д����
        policy
        .WithOrigins("http://127.0.0.1:8080", "http://localhost:8080", "http://127.0.0.1:3000", "http://localhost:3000")
        .AllowAnyHeader()//�����κα�ͷ
        .AllowAnyMethod();//�����κη���
    });
});
#region ApiVersion
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
});
# endregion 
var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerExtension(apiVersionDescriptionProvider);
}
//�� CORS �м����ӵ� web Ӧ�ó��������, �������������
app.UseCors("LimitRequests");
app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<JwtMiddleware>();
app.UseAuthorization();
app.MapControllers();

var name = Assembly.GetExecutingAssembly().GetName();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .Enrich.WithMachineName()
    .Enrich.WithProperty("Assembly", $"{name.Name}")
    .Enrich.WithProperty("Assembly", $"{name.Version}")
    .WriteTo.File(
            new CompactJsonFormatter(),
            Environment.CurrentDirectory + @"/Logs/log.json",
            rollingInterval: RollingInterval.Day,
            restrictedToMinimumLevel: LogEventLevel.Information)
    .WriteTo.Console()
    .CreateLogger();


try
{
    Log.Information("Starting host");
    using (var scope = builder.Services.BuildServiceProvider().CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();
        if (context.Database.IsSqlServer())
            await context.Database.MigrateAsync();
        await DbContextSeed.SeedSampleDataAsync(context);
    }
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}