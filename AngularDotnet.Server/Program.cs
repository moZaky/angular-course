using AngularDotnet.Core;
using AngularDotnet.Server;

var builder = WebApplication.CreateBuilder(args);


IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Environment.CurrentDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)//To specify environment

    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptionsBinders(configuration);
builder.Services.AddDatabase(configuration);
builder.Services.AddServices(configuration);


//builder.Services.AddAuth(configuration);
builder.Services.AddSignalR();
builder.Services.AddAutoMapper(configuration);





var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapHub<ImportNotificationHub>("/Notify");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
