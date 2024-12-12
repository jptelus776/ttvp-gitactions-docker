using EntityInfoService.DAL.CassandraDB;
using EntityInfoService.DAL.MySql;
using EntityInfoService.Filters;
using EntityInfoService.Models;
using EntityInfoService.Utils;
using Microsoft.AspNetCore.Http.Extensions;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;

const string AppName= "entity-info-ms";
const string PathBase = "/" + AppName;

var formatter = new RenderedCompactJsonFormatter(valueFormatter: new JsonValueFormatter());
Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               .WriteTo.Console(formatter)
               .Enrich.FromLogContext()
               .Enrich.With<LogLevelEnricher>()
               .Enrich.WithProperty("AppName", AppName)               
               .CreateBootstrapLogger();
var _logger = Log.Logger.ForContext<Program>();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, svc, lc) => lc
                    .ReadFrom.Configuration(ctx.Configuration)
                    .ReadFrom.Services(svc));

    _logger.Information("Service is starting at {StartTime}", DateTime.UtcNow);

    MySqlHelper.InitializeEnvVariables();
    CassandraHelper.InitalizeEnvVariables();

    // Add services to the container.

    builder.Services.AddControllers()
        .AddJsonOptions(MiddlewareUtility.ConfigureDefaultJsonOptions())
        .AddMvcOptions(options =>
        {
            options.Filters.Add(typeof(ApiResponseExceptionFilter));        // Add Exception Filter.
        });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // generate lowercase URLs
    builder.Services.Configure<RouteOptions>(options =>
    {
        options.LowercaseUrls = true;
    });

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    app.UsePathBase(new PathString(PathBase));

    _ = app.Use(async (context, next) =>
    {
        // Log start of any API call if log level is set to debug.
        _logger.Debug("[API_START] - {RequestMethod} {RequestUrl}", context.Request.Method, context.Request.GetDisplayUrl());
        
        if (context.Request.PathBase.Equals(PathBase))
        {            
            await next.Invoke();         
        }
        else
        {
            context.Response.StatusCode = 404;
            context.Response.Headers.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(ErrorResponseModel.GetResourceNotFoundErrorResponse()).ConfigureAwait(false);
        }

        // Log end of any API if log level is set to debug.
        _logger.Debug("[API_END] - {RequestMethod} {RequestUrl}", context.Request.Method, context.Request.GetDisplayUrl());
    });

    // Add global exception handler
    _ = app.UseExceptionHandler(MiddlewareUtility.GetExceptionHandlerResponse);

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Get standard http status code reponse.
    app.UseStatusCodePages(ctx =>
    {
        return MiddlewareUtility.GetStatusCodePagesResponse(ctx);
    });

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal("Unhandled Exception. Message: {Exception}", ex.Message);
}
finally
{
    Log.Information("Shut Down complete at {StopTime}", DateTime.UtcNow);
    Log.CloseAndFlush();
}