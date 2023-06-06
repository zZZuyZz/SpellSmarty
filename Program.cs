using AutoMapper;
using MediatR;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Infrastructure;
using SpellSmarty.Infrastructure.Repositories;
using SpellSmarty.Infrastructure.Data;
using SpellSmarty.Application.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using SpellSmarty.Application.Queries;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.QueryHandlers;
using SpellSmarty.Infrastructure.Mapper;
using SpellSmarty.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SpellSmartyContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("SpellSmarty")
));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
builder.Services.AddScoped<IRequestHandler<GetVideosQuery, IEnumerable<VideoDto>>, GetVideosHandler>();
builder.Services.AddScoped<IRequestHandler<GetVideosByUserIdQuery, IEnumerable<VideoDto>>, GetVideosByUserIdHandler>();
builder.Services.AddScoped<IRequestHandler<GetSingleVideoQuery, VideoDto>, GetSingleVideoHandler>();
builder.Services.AddScoped<IRequestHandler<GetVideosByCreatorQuery, IEnumerable<VideoDto>>, GetVideosByCreatorHandler>();
builder.Services.AddScoped<IRequestHandler<SaveProgressQuery, VideoStatDto>, SaveProgressHandler>();



// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


// Configure AutoMapper
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<VideoMappingProfile>();
    cfg.AddProfile<VideoStatMappingProfile>();
    cfg.AddProfile<VideoMapper>();
    cfg.AddProfile<VideoStatMapper>();
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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
