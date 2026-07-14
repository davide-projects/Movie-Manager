using Microsoft.EntityFrameworkCore;
using MovieManager.BLL.Models;
using MovieManager.BLL.Services;
using MovieManager.BLL.Services.Interfaces;
using MovieManager.DAL.Data;
using MovieManager.DAL.Entities;
using MovieManager.DAL.Repositories;
using MovieManager.DAL.Repositories.Interfaces;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MovieDBString")
    ?? throw new InvalidOperationException("Connection string \'MovieDBString\' not found.");

builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IGenericService<ActorModel>, GenericService<Actor, ActorModel>>();
builder.Services.AddScoped<IGenericService<DirectorModel>, GenericService<Director, DirectorModel>>();
builder.Services.AddScoped<IGenericService<GenreModel>, GenericService<Genre, GenreModel>>();
builder.Services.AddScoped<IGenericService<MovieModel>, GenericService<Movie, MovieModel>>();
builder.Services.AddScoped<IGenericService<ReviewModel>, GenericService<Review, ReviewModel>>();

builder.Services.AddScoped<IMovieActorRepository, MovieActorRepository>();
builder.Services.AddScoped<IMovieActorService, MovieActorService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddOpenApi();

builder.Services.AddControllers();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();
app.UseAuthorization(); 
app.MapControllers();

app.Run();
