using EFCoreMoviesWebApi;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    //ignore json cyclick dependcy of object
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

//UseNetTopologySuite() is used to configure location (lat/long) data
//for that require nuget package is Microsoft.EntityFrameworkCore.SqlServer.NetTopologySuite

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    //Congigure globally Readonly queries with no tracking boosts read operation
    //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    //for lazy loading using Microsoft.EntityFrameworkCore.Proxies nuget package
    //options.UseLazyLoadingProxies();
    options.UseSqlServer("name=DefaultConnection", sqlserver => sqlserver.UseNetTopologySuite()); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
