using LeaveManagementSystem.API;
using LeaveManagementSystem.API.Configuration;
using LeaveManagementSystem.API.Middleware;
using LeaveManagementSystem.Model.Config;
using LeaveManagementSystem.Service.Logger;
using NLog;

var builder = WebApplication.CreateBuilder(args);


// Logger Configuration
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
RegisterService.RegisterServices(builder.Services);


builder.Services.Configure<DataConfig>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.ConfigureLoggerService();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<LoggerService, LoggerService>();


// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllRequests", builder =>
    {
        builder.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
        .WithExposedHeaders("content-disposition");
    });
});

// Logger Configuration
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

// Add Open API config
builder.Services.AddOpenApiConfiguration(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<CustomMiddleware>();

app.UseOpenApiConfiguration();

app.UseCors("AllRequests");
app.UseRouting();

app.UseAuthorization();
app.UseHttpsRedirection();

string startupPath = System.IO.Directory.GetCurrentDirectory();
string ExceptionLogsPath = startupPath + "\\Logs\\ExceptionLogs";
if (!Directory.Exists(ExceptionLogsPath))
{
    System.IO.Directory.CreateDirectory(ExceptionLogsPath);
}
string RequestLogsPath = startupPath + "\\Logs\\RequestLogs";
if (!Directory.Exists(RequestLogsPath))
{
    System.IO.Directory.CreateDirectory(RequestLogsPath);
}

app.UseStaticFiles();

app.MapControllers();

app.Run();
