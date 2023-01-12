using addons_ts_lab_puppiesApi.Data;
using addons_ts_lab_puppiesApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// I Injected (dependency injection) following service for dependency Injection

builder.Services.AddScoped<IPuppyService, PuppyService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//I Injected (dependency injection) to connect with Sql Server

builder.Services.AddDbContext<PuppyDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("PuppiesApiConnectionString")));


//I added to communicate with react
builder.Services.AddCors();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Images")),
    RequestPath = "/Images"
});
// added folowing line to connect with react
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
