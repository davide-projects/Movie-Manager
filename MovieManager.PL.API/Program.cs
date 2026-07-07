using Microsoft.EntityFrameworkCore;
using MovieManager.BLL.Services.Interfaces;
using MovieManager.DAL.Data;
using MovieManager.DAL.Repositories;
using MovieManager.DAL.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MovieDBString")
    ?? throw new InvalidOperationException("Connection string 'MovieDBString' not found.");

builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
