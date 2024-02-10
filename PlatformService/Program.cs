using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

// Http Client
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

// DB Connection
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseInMemoryDatabase("InMem");
});

// Add Controllers
builder.Services.AddControllers();

// Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

Console.WriteLine($"--> Command Service Endpoint: {builder.Configuration["CommandService"]}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

PrepDB.PrepPopulation(app);

app.MapControllers();

app.Run();
